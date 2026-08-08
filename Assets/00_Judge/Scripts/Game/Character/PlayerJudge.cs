using UnityEngine;

namespace Judge
{
    public class PlayerJudge : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;

        public Transform CameraTarget => _cameraTarget != null ? _cameraTarget : transform;
    }
}
