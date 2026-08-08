using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Judge
{
    public class EvidenceUI : UIBase
    {
        [SerializeField] TextMeshProUGUI _titleText;
        [SerializeField] TextMeshProUGUI _descText;
		[SerializeField] Button _closeButton;

		private void Awake()
		{
            _closeButton.onClick.AddListener(OnClickHide);
		}

		public void Intialize(CharacterType assistantJudgeType)
        {
            RefreshTitleText(assistantJudgeType);
            RefreshDescText(assistantJudgeType);
		}

        private void RefreshTitleText(CharacterType assistantJudgeType)
        {
            _titleText.text = LocalizationManager.GetLocalizedCharacterType(assistantJudgeType) + "ÀÇ ÀÇ°ß";
		}

        private void RefreshDescText(CharacterType assistantJudgeType)
        {
            _descText.text = DataManager.Instance.GetEvidenceStatement(assistantJudgeType);
		}

        private void OnClickHide()
        {
            IngameCameraController.Instance.SetDefaultCameraOn();
            UIManager.Instance.Hide(UIList.EvidenceUI);
            UIManager.Instance.Show(UIList.CasePopupUI);
		}
	}
}
