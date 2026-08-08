using UnityEngine;
using UnityEngine.InputSystem;

namespace Judge
{
    public class RayCastManager : SingletonBase<RayCastManager>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _clickLayerMask = ~0;
        [SerializeField] private float _rayDistance = 100.0f;

		protected override void Awake()
		{
            base.Awake();

			if (_camera == null)
                _camera = Camera.main;
		}
		private void Update()
        {
            if (!TryGetPointerDownPosition(out Vector2 screenPosition))
                return;

            TryClickAssistantJudge(screenPosition);
        }

        private bool TryGetPointerDownPosition(out Vector2 screenPosition)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                screenPosition = Mouse.current.position.ReadValue();
                return true;
            }

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                return true;
            }

            screenPosition = Vector2.zero;
            return false;
        }

        private void TryClickAssistantJudge(Vector2 screenPosition)
        {
            if (GameManager.Instance.CurrentGameState != GameState.Hearing)
                return;

            if (UIManager.Instance.IsActive(UIList.EvidenceUI))
                return;

            if (IngameCameraController.HasInstance && IngameCameraController.Instance.IsBlending)
                return;

			Ray ray = _camera.ScreenPointToRay(screenPosition);

            TryClickAssistantJudge2D(ray);
        }

        private bool TryClickAssistantJudge2D(Ray ray)
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, _rayDistance, _clickLayerMask);

            if (hit.collider == null)
            {
                return false;
            }

            AssistantJudge assistantJudge = hit.collider.GetComponentInParent<AssistantJudge>();

            if (assistantJudge == null)
            {
                return false;
            }

            assistantJudge.OnClick();
            return true;
        }
    }
}
