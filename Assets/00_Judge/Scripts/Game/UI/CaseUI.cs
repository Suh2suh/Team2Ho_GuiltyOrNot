using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Judge
{
    public class CaseUI : UIBase
    {
        [SerializeField] GameObject _page1Group;
        [SerializeField] GameObject _page2Group;
        [Space(5)]
        [SerializeField] Button _moveToPage1Button;
        [SerializeField] Button _moveToPage2Button;
        [SerializeField] Button _caseStartButton;
		[Space(5)]
        [SerializeField] TextMeshProUGUI _caseTitleText;
        [SerializeField] TextMeshProUGUI _caseDescText;
        [SerializeField] TextMeshProUGUI _caseTopicText;
        [SerializeField] TextMeshProUGUI _caseEvidenceText;


		private void Awake()
		{
			_moveToPage1Button.onClick.AddListener(OnClickMoveToPage1);
			_moveToPage2Button.onClick.AddListener(OnClickMoveToPage2);
			_caseStartButton.onClick.AddListener(OnClickCaseStart);
		}

		public override void Show()
		{
			base.Show();

			Initialize();
			OnClickMoveToPage1();
		}

		// public void Initialize(caseID)  // 추후 caseID로 Show 전 Initialize
		private void Initialize()  // 추후 caseID로 Show 전 Initialize
        {

			CaseData caseData = DataManager.Instance.CaseData;
			if (caseData == null) return;

			_caseTitleText.text = caseData.Title;
			_caseDescText.text = caseData.Summary;
			_caseTopicText.text = caseData.JudgmentTarget;
			_caseEvidenceText.text = caseData.Evidence?.Description ?? string.Empty;
		}

        public void OnClickMoveToPage1()
        {
			SetActivePage2(false);
			SetActivePage1(true);
		}

		public void OnClickMoveToPage2()
		{
			SetActivePage1(false);
			SetActivePage2(true);
		}

		public void OnClickCaseStart()
		{
			UIManager.Instance.Hide(UIList.CaseUI);
			GameManager.Instance.SetGameState(GameState.Hearing);
		}

		private void SetActivePage1(bool activeSelf)
            => _page1Group.SetActive(activeSelf);

		private void SetActivePage2(bool activeSelf)
			=> _page2Group.SetActive(activeSelf);
	}
}
