using Cysharp.Threading.Tasks;

namespace Judge
{
    public abstract class SceneFlowBase
    {
        public virtual bool UseLoadingUI => false;

        public virtual UniTask PreProcessAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask MainProcessAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask PostProcessAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask PostDestroyAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}
