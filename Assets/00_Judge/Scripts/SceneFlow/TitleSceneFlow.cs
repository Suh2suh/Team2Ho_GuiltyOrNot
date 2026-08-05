using Cysharp.Threading.Tasks;

namespace Judge
{
    public class TitleSceneFlow : SceneFlowBase
    {
        public override UniTask PostProcessAsync()
        {
            UIManager.Instance.Show(UIList.TitleUI);
            return UniTask.CompletedTask;
        }

        public override UniTask PostDestroyAsync()
        {
            if (UIManager.HasInstance)
            {
                UIManager.ExistingInstance.Hide(UIList.TitleUI);
            }

            return UniTask.CompletedTask;
        }
    }
}
