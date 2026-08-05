using UnityEngine;

namespace Judge
{
    public class RayCastManager : SingletonBase<RayCastManager>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _clickLayerMask = ~0;
        [SerializeField] private float _rayDistance = 100.0f;

        private Camera CurrentCamera => _camera != null ? _camera : Camera.main;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            TryClickAssistantJudge();
        }

        private void TryClickAssistantJudge()
        {
            Camera currentCamera = CurrentCamera;

            if (currentCamera == null)
            {
                return;
            }

            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

            if (TryClickAssistantJudge3D(ray))
            {
                return;
            }

            TryClickAssistantJudge2D(ray);
        }

        private bool TryClickAssistantJudge3D(Ray ray)
        {
            if (!Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _clickLayerMask))
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
