using UnityEngine;

namespace Judge
{
    public class Main : MonoBehaviour
    {
        private void Start()
        {
            InitializeManagers();
            SceneFlowManager.Instance.Load(SceneList.TitleScene);
        }

        private void InitializeManagers()
        {
            _ = UIManager.Instance;
            _ = SceneFlowManager.Instance;
        }
    }
}
