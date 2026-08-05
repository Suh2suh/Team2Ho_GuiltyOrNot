using System;
using UnityEngine;

namespace Judge
{
    public class GameManager : SingletonBase<GameManager>
    {
		[SerializeField] private GameState _currentGameState = GameState.None;
		private GameState _prevGameState = GameState.None;

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

            OnGameStateChanged?.Invoke(_prevGameState, _currentGameState);
        }

        public void ClearGameState()
        {
            SetGameState(GameState.None);
        }
    }
}
