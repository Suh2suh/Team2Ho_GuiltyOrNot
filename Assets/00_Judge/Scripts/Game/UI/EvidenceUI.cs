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
            _titleText.text = LocalizationManager.GetLocalizedCharacterType(assistantJudgeType) + "의 의견";
		}

        private void RefreshDescText(CharacterType assistantJudgeType)
        {
            _descText.text = string.Empty; // TODO: 우항 = Resources/Data/evidenceDataJson.json 로드, evidenceStatements.assistantJudgeType(loswerCase 변환 필요) 출력
		}

        private void OnClickHide()
        {
            IngameCameraController.Instance.SetDefaultCameraOn();
            UIManager.Instance.Hide(UIList.EvidenceUI);
            UIManager.Instance.Show(UIList.CasePopupUI);
		}
	}
}
