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
        [SerializeField] GameObject _overallPageGroup;

        [Header("UserInput")]
        [SerializeField] TextMeshProUGUI _caseTitleText;
        [SerializeField] Toggle _guiltyToggle;
        [SerializeField] Toggle _notGuiltyToggle;
		[SerializeField] TMP_InputField _userInputField;
        [SerializeField] Button _submitButton;
		[SerializeField] Button _toOverallPageButton;
		[SerializeField] Button _doneGamePhaseButton;
        [SerializeField] private List<TagToggle> _tagToggles = new();

		[Header("Result")] 
        [SerializeField] List<AssistantCommentArea> _assistantCommentAreas;
        [Serializable]
        private class AssistantCommentArea
		{
            public CharacterType CharacterType;
            public TextMeshProUGUI ScoreText;
            public TextMeshProUGUI CommentText;
        }

        [Header("Result")]
        [SerializeField] TextMeshProUGUI _overallScoreText;
        [SerializeField] TextMeshProUGUI _overallCommentText;


		private bool _isGuilty = true;
        private bool IsGuilty
        {
            get => _isGuilty;
			set
            {
                if (_isGuilty != value)
                {
					ClearJudgePage();
                    _isGuilty = value;
				}
            }
        }
		private HashSet<string> _selectedTagIDs = new();


		private void Awake()
		{
            _guiltyToggle.onValueChanged.AddListener(OnClickGuiltyToggle);
			void OnClickGuiltyToggle(bool isOn)
            {
                if (isOn) IsGuilty = true;
			}
            _notGuiltyToggle.onValueChanged.AddListener(OnClickNotGuiltyToggle);
			void OnClickNotGuiltyToggle(bool isOn)
			{
				if (isOn) IsGuilty = false;
			}

			_submitButton.onClick.AddListener(OnClickSubmit);
            _toOverallPageButton.onClick.AddListener(OnClickToOverallPage);
            _doneGamePhaseButton.onClick.AddListener(OnClickDoneGamePhase);
		}

		public override void Show()
		{
			base.Show();

            ClearJudgePage();
            UpdateJudgePage();

			SetActiveJudgePage(true);
            SetActiveResultPage(false);
            SetActiveOverallPage(false);
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

                commentArea.ScoreText.text = evaluation.Score + "점";
                commentArea.CommentText.text = evaluation.Reaction;
            }
        }

        private void UpdateOverallPage()
        {
            int overallScore = 0;
			foreach (AssistantCommentArea commentArea in _assistantCommentAreas)
			{
				CharacterEvaluationData evaluation = DataManager.Instance.GetCharacterEvaluation(commentArea.CharacterType);
                overallScore += evaluation.Score;
			}
            _overallScoreText.text = overallScore.ToString() + "점";
            _overallCommentText.text = DataManager.Instance.ResultData.OverallComment;
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
			// await API 수신

			UpdateResultPage();
			SetActiveResultPage(true);
        }

		private void OnClickToOverallPage()
		{
			SetActiveResultPage(false);

            UpdateOverallPage();
            SetActiveOverallPage(true);
		}

		private void OnClickDoneGamePhase()
        {
            GameManager.Instance.SetGameState(GameState.None);
		}


		private void SetActiveJudgePage(bool activeSelf)
			=> _judgePageGroup.SetActive(activeSelf);
		private void SetActiveResultPage(bool activeSelf)
			=> _resultPageGroup.SetActive(activeSelf);
		private void SetActiveOverallPage(bool activeSelf)
			=> _overallPageGroup.SetActive(activeSelf);
	}
}
