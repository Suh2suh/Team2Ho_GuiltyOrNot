using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Judge
{
    public class CasePopupUI : UIBase
    {
		[SerializeField] TextMeshProUGUI _caseTitleText;
		[SerializeField] TextMeshProUGUI _caseTopicText;
		[SerializeField] TextMeshProUGUI _caseEvidenceText;
		[SerializeField] Button _verdictStartButton;


		private void Awake()
		{
			_verdictStartButton.onClick.AddListener(OnClickVerdictStart);
		}

		public override void Show()
		{
			base.Show();

			Initialize();
			SetActiveVerdictStartButton(true);
		}


		// public void Initialize(caseID)  // 추후 caseID로 Show 전 Initialize
		private void Initialize()
		{

			CaseData caseData = DataManager.Instance.CaseData;
			if (caseData == null) return;

			_caseTitleText.text = caseData.Title;
			_caseTopicText.text = caseData.JudgmentTarget;
			_caseEvidenceText.text = caseData.Evidence?.Description ?? string.Empty;
		}

		private void OnClickVerdictStart()
		{
			SetActiveVerdictStartButton(false);
			GameManager.Instance.SetGameState(GameState.Verdict);
		}

		private void SetActiveVerdictStartButton(bool activeSelf)
			=> _verdictStartButton.gameObject.SetActive(activeSelf);

    }

}
