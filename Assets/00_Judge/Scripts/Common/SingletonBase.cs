using UnityEngine;

namespace Judge
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isApplicationQuitting;

        public static T Instance
        {
            get
            {
                if (_isApplicationQuitting)
                {
                    return null;
                }

                if (_instance != null)
                {
                    return _instance;
                }

                T[] foundInstances = FindObjectsOfType<T>();

                if (foundInstances.Length > 0)
                {
                    _instance = foundInstances[0];
                }
                else
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }

                return _instance;
            }
        }

        public static bool HasInstance => _instance != null;

        public static T ExistingInstance => _instance;

        protected virtual bool PersistAcrossScenes => false;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                if (PersistAcrossScenes && gameObject.scene.name != "DontDestroyOnLoad")
                {
                    DontDestroyOnLoad(gameObject);
                }

                Initialize();
                return;
            }

            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public virtual void Initialize()
        {
        }

        protected virtual void OnApplicationQuit()
        {
            _isApplicationQuitting = true;
        }
    }
}
