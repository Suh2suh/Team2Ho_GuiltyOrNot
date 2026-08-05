using UnityEngine;

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
            if (GameManager.Instance.CurrentGameState != GameState.Hearing)
                return;

            if (!Input.GetMouseButtonDown(0))
                return;

            TryClickAssistantJudge();
        }

        private void TryClickAssistantJudge()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

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
