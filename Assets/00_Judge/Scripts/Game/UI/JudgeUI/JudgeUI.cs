using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Judge
{
    public class JudgeUI : UIBase
    {
        [SerializeField] GameObject _judgePageGroup;
        [SerializeField] GameObject _resultPageGroup;

        [Header("UserInput")]
        [SerializeField] TextMeshProUGUI _caseTitleText;
        [SerializeField] Toggle _guiltyToggle;
        [SerializeField] Toggle _notGuiltyToggle;
		[SerializeField] TMP_InputField _userInputField;
        [SerializeField] Button _submitButton;
		[SerializeField] Button _okayButton;
        [SerializeField] private List<TagToggle> _tagToggles = new();
        private HashSet<string> _selectedTagIDs = new();

		[Header("Result")] 
        [SerializeField] List<AssistantCommentArea> _assistantCommentAreas;
        [Serializable]
        private class AssistantCommentArea
		{
            public CharacterType CharacterType;
            public TextMeshProUGUI ScoreText;
            public TextMeshProUGUI CommentText;
        }

		private void Awake()
		{
            _guiltyToggle.onValueChanged.AddListener(WrapperGuiltToggle);	
            void WrapperGuiltToggle(bool b)
            {
                ClearJudgePage();
			}

            _submitButton.onClick.AddListener(OnClickSubmit);
            _okayButton.onClick.AddListener(OnClickOK);
		}

		public override void Show()
		{
			base.Show();

            UpdateJudgePage();

			SetActiveJudgePage(true);
            SetActiveResultPage(false);
		}

        private void UpdateJudgePage()
        {
            _selectedTagIDs.Clear();
            // caseDataJson 읽기
			foreach (var tagToggle in _tagToggles)
            {
                // availableTags 순회, id/label 하나씩 넣기
                tagToggle.Initialize(string.Empty, string.Empty);
			}
		}

		private void ClearJudgePage()
        {
            _selectedTagIDs.Clear();

            foreach (var tag in _tagToggles)
            {
                tag.Toggle.SetIsOnWithoutNotify(false);
            }

            _userInputField.text = string.Empty;
        }

        private void UpdateResultPage()
        {
			foreach (var commentArea in _assistantCommentAreas)
			{
			    // Assets/Resources/Data/resultJson.json 의 characterEvaluations.CharacterType별로 출력
                commentArea.ScoreText.text = string.Empty + "점";
                commentArea.CommentText.text = string.Empty;
			}
		}

        public void OnClickTagToggle(TagToggle tagToggle, bool isOn)
        {
            string tagID = tagToggle.TagID;

            if (isOn)
            {
				_selectedTagIDs.Add(tagID);
			}
            else
            {
				if (_selectedTagIDs.Contains(tagID))
				{
					_selectedTagIDs.Remove(tagID);
				}
			}
		}

        private void OnClickSubmit()
        {
            SetActiveJudgePage(false);

			// Assets/Resources/Data/userInputJson.json에 정보 저장
			// await API 수신

			UpdateResultPage();
			SetActiveResultPage(true);
        }

        private void OnClickOK()
        {
            GameManager.Instance.SetGameState(GameState.None);
		}


		private void SetActiveJudgePage(bool activeSelf)
			=> _judgePageGroup.SetActive(activeSelf);
		private void SetActiveResultPage(bool activeSelf)
			=> _resultPageGroup.SetActive(activeSelf);
	}
}
