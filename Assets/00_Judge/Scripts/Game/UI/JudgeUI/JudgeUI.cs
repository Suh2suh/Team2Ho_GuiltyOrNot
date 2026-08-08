using System;
using System.Collections.Generic;
using TMPro;
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

            ClearJudgePage();
            UpdateJudgePage();

			SetActiveJudgePage(true);
            SetActiveResultPage(false);
		}

        private void UpdateJudgePage()
        {
            _selectedTagIDs.Clear();
            CaseData caseData = DataManager.Instance.CaseData;
            List<TagData> availableTags = caseData?.AvailableTags;
            _caseTitleText.text = caseData?.Title ?? string.Empty;

            for (int i = 0; i < _tagToggles.Count; i++)
            {
                bool hasTagData = availableTags != null && i < availableTags.Count;
                _tagToggles[i].gameObject.SetActive(hasTagData);

                if (hasTagData)
                {
                    TagData tagData = availableTags[i];
                    _tagToggles[i].Initialize(tagData.ID, tagData.Label);
                }
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
            foreach (AssistantCommentArea commentArea in _assistantCommentAreas)
            {
                CharacterEvaluationData evaluation = DataManager.Instance.GetCharacterEvaluation(commentArea.CharacterType);

                if (evaluation == null)
                {
                    commentArea.ScoreText.text = string.Empty;
                    commentArea.CommentText.text = string.Empty;
                    continue;
                }

                commentArea.ScoreText.text = evaluation.Score + "Á¡";
                commentArea.CommentText.text = evaluation.Reaction;
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

            string verdict = _guiltyToggle.isOn ? "GUILTY" : "NOT_GUILTY";
            DataManager.Instance.UpdateUserInput(
                GameManager.Instance.CaseID,
                verdict,
                _selectedTagIDs,
                _userInputField.text);
			// await API ¼ö½Å

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
