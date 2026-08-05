using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Judge
{
    public class TitleUI : UIBase
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _onClickToStart;
        [SerializeField] private float _fadeInOutDuration = 1.0f;

        private CancellationTokenSource _fadeCancellationTokenSource;

        private void OnEnable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(LoadIngameScene);
            }
        }

        private void OnDisable()
        {
            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(LoadIngameScene);
            }
        }

        private void OnDestroy()
        {
            StopFadeInOutText();
        }

        public override void Show()
        {
            base.Show();
            StartFadeInOutText();
        }

        public override void Hide()
        {
            StopFadeInOutText();
            base.Hide();
        }

        public void StartFadeInOutText()
        {
            StopFadeInOutText();

            if (_onClickToStart == null)
            {
                return;
            }

            SetTextAlpha(1.0f);
            _fadeCancellationTokenSource = new CancellationTokenSource();
            FadeInOutTextAsync(_fadeCancellationTokenSource.Token).Forget();
        }

        public void StopFadeInOutText()
        {
            if (_fadeCancellationTokenSource == null)
            {
                return;
            }

            _fadeCancellationTokenSource.Cancel();
            _fadeCancellationTokenSource.Dispose();
            _fadeCancellationTokenSource = null;
        }

        private void LoadIngameScene()
        {
            SceneFlowManager.Instance.Load(SceneList.IngameScene);
        }

        private async UniTask FadeInOutTextAsync(CancellationToken cancellationToken)
        {
            try
            {
                float duration = Mathf.Max(_fadeInOutDuration, 0.01f);
                float halfDuration = duration * 0.5f;

                while (!cancellationToken.IsCancellationRequested)
                {
                    await FadeTextAlphaAsync(1.0f, 0.0f, halfDuration, cancellationToken);
                    await FadeTextAlphaAsync(0.0f, 1.0f, halfDuration, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async UniTask FadeTextAlphaAsync(float startAlpha, float endAlpha, float duration, CancellationToken cancellationToken)
        {
            float elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                cancellationToken.ThrowIfCancellationRequested();

                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                SetTextAlpha(Mathf.Lerp(startAlpha, endAlpha, t));

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            SetTextAlpha(endAlpha);
        }

        private void SetTextAlpha(float alpha)
        {
            Color32 color = _onClickToStart.color;
            color.a = (byte)Mathf.RoundToInt(Mathf.Clamp01(alpha) * byte.MaxValue);
            _onClickToStart.color = color;
        }
    }
}
