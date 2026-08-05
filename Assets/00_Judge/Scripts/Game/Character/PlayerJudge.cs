using UnityEngine;

namespace Judge
{
    public class Judge : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;

        public Transform CameraTarget => _cameraTarget != null ? _cameraTarget : transform;
    }
}
