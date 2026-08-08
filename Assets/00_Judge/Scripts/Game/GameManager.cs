using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Judge
{
    public class GameManager : SingletonBase<GameManager>
    {
		[SerializeField] private string _caseID = "case_001_last_tangsuyuk";
		[SerializeField] private GameState _currentGameState = GameState.None;
		private GameState _prevGameState = GameState.None;

        public string CaseID => _caseID;
        public GameState CurrentGameState => _currentGameState;
        public GameState PrevGameState => _prevGameState;

        public event Action<GameState, GameState> OnGameStateChanged;

        public void SetGameState(GameState gameState)
        {
            if (_currentGameState == gameState)
            {
                return;
            }

            _prevGameState = _currentGameState;
            _currentGameState = gameState;

            switch (_currentGameState)
            {
                case GameState.None:
                    if (_prevGameState == GameState.Verdict)
                        StartEndFlow().Forget();
                    break;

                case GameState.CaseBriefing:
                    StartCaseBreifing().Forget();
                    break;

                case GameState.Hearing:
                    StartHearing().Forget();
                    break;

                case GameState.Verdict: 
                    StartVerdict().Forget();
                    break;
            }

            OnGameStateChanged?.Invoke(_prevGameState, _currentGameState);
        }

        public void ClearGameState()
        {
            SetGameState(GameState.None);
        }

        private async UniTask StartCaseBreifing()
        {
            await UniTask.WaitForSeconds(1.5f);

            await IngameCameraController.Instance.SetCameraOnAsync(CharacterType.Judge);

			UIManager.Instance.Show(UIList.CaseUI);
        }

        private async UniTask StartHearing()
        {
            await IngameCameraController.Instance.SetDefaultCameraOnAsync();

			foreach (CharacterType characterType in Enum.GetValues(typeof(CharacterType)))
            {
                if (characterType == CharacterType.Judge) continue;

                var assistantJudge = IngameSceneController.Instance.GetAssistantJudge(characterType);
                assistantJudge.ShowBubble();
			}

            UIManager.Instance.Show(UIList.CasePopupUI);
        }

		private async UniTask StartVerdict()
		{
			await IngameCameraController.Instance.SetCameraOnAsync(CharacterType.Judge);

			var caseUI = UIManager.Instance.Get<CaseUI>(UIList.CaseUI);
			UIManager.Instance.Show(UIList.CaseUI);
		}

        private async UniTask StartEndFlow()
        {
            UIManager.Instance.Hide(UIList.JudgeUI);
            UIManager.Instance.Hide(UIList.CasePopupUI);

			await IngameCameraController.Instance.SetDefaultCameraOnAsync();

			SceneFlowManager.Instance.Load(SceneList.TitleScene);
		}
	}
}
