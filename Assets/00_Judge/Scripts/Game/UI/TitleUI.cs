using DG.Tweening;
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

        private Tween _fadeTween;

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

            float halfDuration = Mathf.Max(_fadeInOutDuration, 0.01f) * 0.5f;
            _fadeTween = _onClickToStart.DOFade(0.0f, halfDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        public void StopFadeInOutText()
        {
            if (_fadeTween != null)
            {
                _fadeTween.Kill();
                _fadeTween = null;
            }

            if (_onClickToStart != null)
            {
                SetTextAlpha(1.0f);
            }
        }

        private void LoadIngameScene()
        {
            SceneFlowManager.Instance.Load(SceneList.IngameScene);
        }

        private void SetTextAlpha(float alpha)
        {
            Color32 color = _onClickToStart.color;
            color.a = (byte)Mathf.RoundToInt(Mathf.Clamp01(alpha) * byte.MaxValue);
            _onClickToStart.color = color;
        }
    }
}
