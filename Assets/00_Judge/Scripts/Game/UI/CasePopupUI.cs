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
			// Assets/Resources/Data/caseDataJson.json을 읽는다
			// 아래 모든 텍스트는 caseDataJson.json에서 parsing해온 값을 입력한다

			_caseTitleText.text = string.Empty;      //caseDataJson.json의 title
			_caseTopicText.text = string.Empty;      //caseDataJson.json의 judgmentTarget
			_caseEvidenceText.text = string.Empty;//caseDataJson.json의 evidence.description
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
