using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace Judge
{
    [Serializable]
    public class CameraByCharacter
    {
        public CharacterType CharacterType;
        public CinemachineCamera Camera;
    }

    public class IngameCameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [SerializeField] private CinemachineCamera _defaultCamera;
        [SerializeField] private List<CameraByCharacter> _cameraByCharacterList = new List<CameraByCharacter>();

        private CinemachineCamera _currentCamera;
        private CinemachineCamera _prevCamera;

        public CinemachineCamera CurrentCamera => _currentCamera;
        public CinemachineCamera PrevCamera => _prevCamera;

        private void Awake()
        {
            CacheCurrentCamera();
        }

        private void Start()
        {
            StartIngameCameraFlowAsync().Forget();
        }

        public void SetCameraOn(CharacterType characterType)
        {
            CinemachineCamera camera = FindCamera(characterType);

            if (camera == null)
            {
                Debug.LogWarning($"Camera is not registered. CharacterType: {characterType}");
                return;
            }

            SetCameraOn(camera);
        }

        public void SetCameraOn(CinemachineCamera currentCamera)
        {
            if (currentCamera == null || _currentCamera == currentCamera)
            {
                return;
            }

            _prevCamera = _currentCamera;

            if (_prevCamera != null)
            {
                _prevCamera.gameObject.SetActive(false);
            }

            _currentCamera = currentCamera;
            _currentCamera.gameObject.SetActive(true);
        }

        public void SetDefaultCameraOn()
        {
            if (_defaultCamera == null)
            {
                Debug.LogWarning("Default camera is not assigned.");
                return;
            }

            SetCameraOn(_defaultCamera);
        }

        public async UniTask SetCameraOnAsync(CharacterType characterType)
        {
            SetCameraOn(characterType);
            await WaitForBlendAsync();
        }

        public async UniTask SetCameraOnAsync(CinemachineCamera currentCamera)
        {
            SetCameraOn(currentCamera);
            await WaitForBlendAsync();
        }

        public async UniTask SetDefaultCameraOnAsync()
        {
            SetDefaultCameraOn();
            await WaitForBlendAsync();
        }

        public CinemachineCamera FindCamera(CharacterType characterType)
        {
            foreach (CameraByCharacter cameraByCharacter in _cameraByCharacterList)
            {
                if (cameraByCharacter.CharacterType == characterType)
                {
                    return cameraByCharacter.Camera;
                }
            }

            return null;
        }

        public async UniTask WaitForBlendAsync()
        {
            if (_cinemachineBrain == null)
            {
                return;
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.WaitUntil(() => !_cinemachineBrain.IsBlending);
        }

        private async UniTask StartIngameCameraFlowAsync()
        {
            await SetCameraOnAsync(CharacterType.Judge);
            UIManager.Instance.Show(UIList.CaseUI);
        }

        private void CacheCurrentCamera()
        {
            foreach (CameraByCharacter cameraByCharacter in _cameraByCharacterList)
            {
                if (cameraByCharacter.Camera != null && cameraByCharacter.Camera.gameObject.activeSelf)
                {
                    _currentCamera = cameraByCharacter.Camera;

                    if (_defaultCamera == null)
                    {
                        _defaultCamera = _currentCamera;
                    }

                    return;
                }
            }

            if (_defaultCamera != null && _defaultCamera.gameObject.activeSelf)
            {
                _currentCamera = _defaultCamera;
            }
        }
    }
}
