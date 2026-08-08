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
			// Assets/Resources/Data/caseDataJson.json을 읽는다
			// 아래 모든 텍스트는 caseDataJson.json에서 parsing해온 값을 입력한다

			// PAGE 1
			_caseTitleText.text = string.Empty;      //caseDataJson.json의 title
			_caseDescText.text = string.Empty;     //caseDataJson.json의 summary

			// PAGET 2
			_caseTopicText.text = string.Empty;      //caseDataJson.json의 judgmentTarget
			_caseEvidenceText.text = string.Empty;//caseDataJson.json의 evidence.description
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
