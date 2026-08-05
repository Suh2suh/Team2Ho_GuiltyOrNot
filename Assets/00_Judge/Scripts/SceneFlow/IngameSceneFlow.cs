using Cysharp.Threading.Tasks;

namespace Judge
{
    public class IngameSceneFlow : SceneFlowBase
    {
        public override bool UseLoadingUI => true;


		public override UniTask PreProcessAsync()
		{
			return UniTask.CompletedTask;
		}

		public override UniTask MainProcessAsync()
		{
			return UniTask.CompletedTask;
		}

		public override UniTask PostProcessAsync()
		{
			GameManager.Instance.SetGameState(GameState.CaseBriefing);

			return UniTask.CompletedTask;
		}

		public override UniTask PostDestroyAsync()
		{
			return UniTask.CompletedTask;
		}
	}
}
