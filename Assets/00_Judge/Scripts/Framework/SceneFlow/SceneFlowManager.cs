using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Judge
{
    public class SceneFlowManager : SingletonBase<SceneFlowManager>
    {
        protected override bool PersistAcrossScenes => true;

        private bool _isLoading;
        public SceneFlowBase CurrentSceneFlow { get; private set; }


        public void Load(SceneList sceneName)
        {
            LoadAsync(sceneName).Forget();
        }

        public async UniTask LoadAsync(SceneList sceneName)
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;

            SceneFlowBase nextSceneFlow = CreateSceneFlow(sceneName);

            if (nextSceneFlow == null)
            {
                _isLoading = false;
                return;
            }

            SceneFlowBase previousSceneFlow = CurrentSceneFlow;
            bool useLoadingUI = nextSceneFlow.UseLoadingUI;

            if (useLoadingUI)
            {
                UIManager.Instance.Show(UIList.LoadingUI);
            }

            try
            {
                if (previousSceneFlow != null)
                {
                    await previousSceneFlow.PostDestroyAsync();
                }

                CurrentSceneFlow = nextSceneFlow;

                await CurrentSceneFlow.PreProcessAsync();

                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName.ToString());

                if (loadOperation == null)
                {
                    Debug.LogWarning($"Scene load failed. SceneName: {sceneName}");
                    return;
                }

                UniTask mainProcessTask = CurrentSceneFlow.MainProcessAsync();

                await loadOperation.ToUniTask();
                await mainProcessTask;
                await CurrentSceneFlow.PostProcessAsync();
            }
            finally
            {
                if (useLoadingUI && UIManager.HasInstance)
                {
                    UIManager.ExistingInstance.Hide(UIList.LoadingUI);
                }

                _isLoading = false;
            }
        }

        private SceneFlowBase CreateSceneFlow(SceneList sceneName)
        {
            switch (sceneName)
            {
                case SceneList.TitleScene:
                    return new TitleSceneFlow();
                case SceneList.IngameScene:
                    return new IngameSceneFlow();
                default:
                    Debug.LogWarning($"SceneFlow is not registered. SceneName: {sceneName}");
                    return null;
            }
        }
    }
}
