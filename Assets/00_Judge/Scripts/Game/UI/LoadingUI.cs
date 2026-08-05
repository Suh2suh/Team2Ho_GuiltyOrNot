using UnityEngine;

namespace Judge
{
    public class LoadingUI : UIBase
    {
        private float _progress;

        public float Progress => _progress;

        public void SetProgress(float progress)
        {
            _progress = Mathf.Clamp01(progress);
        }
    }
}
