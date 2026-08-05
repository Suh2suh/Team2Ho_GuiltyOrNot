using DG.Tweening;
using UnityEngine;

namespace Judge
{
    public class TalkBubble : MonoBehaviour
    {
        [SerializeField] private float _moveDistance = 0.2f;
        [SerializeField] private float _moveDuration = 0.8f;
        [SerializeField] private float _maxStartDelay = 0.25f;
        [SerializeField] private Ease _ease = Ease.InOutSine;

        private Vector3 _originLocalPosition;
        private Tween _moveTween;

        private void Awake()
        {
            _originLocalPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            StartMoveLoop();
        }

        private void OnDisable()
        {
            StopMoveLoop();
        }

        private void OnDestroy()
        {
            StopMoveLoop();
        }

        private void StartMoveLoop()
        {
            StopMoveLoop();

            transform.localPosition = _originLocalPosition;

            float startDelay = Random.Range(0.0f, _maxStartDelay);
            float targetY = _originLocalPosition.y + _moveDistance;

            _moveTween = transform.DOLocalMoveY(targetY, _moveDuration)
                .SetEase(_ease)
                .SetDelay(startDelay)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        private void StopMoveLoop()
        {
            if (_moveTween != null)
            {
                _moveTween.Kill();
                _moveTween = null;
            }

            transform.localPosition = _originLocalPosition;
        }
    }
}
