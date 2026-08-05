using UnityEngine;

namespace Judge
{
    public class AssistantJudge : MonoBehaviour
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private TalkBubble _talkBubble;
        [SerializeField] private Transform _cameraTarget;

        private bool _hasClicked;

        public CharacterType CharacterType => _characterType;
        public Transform CameraTarget => _cameraTarget != null ? _cameraTarget : transform;
        public bool HasClicked => _hasClicked;

        public void ShowBubble()
        {
            if (_talkBubble != null)
            {
                _talkBubble.gameObject.SetActive(true);
            }
        }

        public void HideBubble()
        {
            if (_talkBubble != null)
            {
                _talkBubble.gameObject.SetActive(false);
            }
        }

        public void ResetClickState()
        {
            _hasClicked = false;
        }

        public void OnClick()
        {
            if (!_hasClicked)
            {
                _hasClicked = true;
                HideBubble();
            }
        }


    }
}
