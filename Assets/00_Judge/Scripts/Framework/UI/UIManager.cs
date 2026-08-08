using System.Collections.Generic;
using UnityEngine;

namespace Judge
{
    public class UIManager : SingletonBase<UIManager>
    {
        protected override bool PersistAcrossScenes => true;

        [SerializeField] private UIDatabase _uiDatabase;
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private Transform _panelRoot;
        [SerializeField] private Transform _popupRoot;

        private readonly Dictionary<UIList, UIBase> _uiInstances = new Dictionary<UIList, UIBase>();


        public override void Initialize()
        {
            if (_uiDatabase == null)
            {
                _uiDatabase = Resources.Load<UIDatabase>("UIDatabase");
            }

            CreateRootsIfNeeded();
        }

        public void Show(UIList uiName)
        {
            UIBase ui = GetOrCreateUI(uiName);

            if (ui == null)
            {
                return;
            }

            ui.Show();
        }

        public void Hide(UIList uiName, bool shouldDestroy = false)
        {
            if (shouldDestroy)
            {
                Destroy(uiName);
                return;
            }

            if (!_uiInstances.TryGetValue(uiName, out UIBase ui) || ui == null)
            {
                return;
            }

            ui.Hide();
        }

		public T Get<T>(UIList uiName) where T : UIBase
		{
			UIBase ui = GetOrCreateUI(uiName);

			if (ui == null)
			{
				return null;
			}

            ui.Hide();
            return ui as T;
		}

		public void Destroy(UIList uiName)
        {
            if (!_uiInstances.TryGetValue(uiName, out UIBase ui))
            {
                return;
            }

            _uiInstances.Remove(uiName);

            if (ui != null)
            {
                UnityEngine.Object.Destroy(ui.gameObject);
            }
        }

        public bool IsActive(UIList uiName)
        {
			if (!_uiInstances.TryGetValue(uiName, out UIBase ui) || ui == null)
			{
				return false;
			}

            return _uiInstances[uiName].gameObject.activeSelf;
		}

		private UIBase GetOrCreateUI(UIList uiName)
        {
            if (_uiInstances.TryGetValue(uiName, out UIBase ui) && ui != null)
            {
                return ui;
            }

            UIData uiData = FindUIData(uiName);

            if (uiData == null || uiData.Prefab == null)
            {
                Debug.LogWarning($"UI prefab is not registered. UIName: {uiName}");
                return null;
            }

            Transform parent = uiName.GetUIType() == UIType.Panel ? _panelRoot : _popupRoot;
            GameObject uiObject = Instantiate(uiData.Prefab, parent);
            uiObject.name = uiName.ToString();

            UIBase uiInstance = uiObject.GetComponent<UIBase>();

            if (uiInstance == null)
            {
                Debug.LogWarning($"UI prefab does not have UIBase component. UIName: {uiName}");
                UnityEngine.Object.Destroy(uiObject);
                return null;
            }

            _uiInstances[uiName] = uiInstance;

            return uiInstance;
        }

        private UIData FindUIData(UIList uiName)
        {
            if (_uiDatabase == null)
            {
                Debug.LogWarning("UIDatabase is not assigned.");
                return null;
            }

            foreach (UIData uiData in _uiDatabase.UIDataList)
            {
                if (uiData.UIName == uiName)
                {
                    return uiData;
                }
            }

            return null;
        }

        private void CreateRootsIfNeeded()
        {
            if (_uiRoot == null)
            {
                _uiRoot = CreateRoot("UIRoot", transform);
            }

            if (_panelRoot == null)
            {
                _panelRoot = CreateRoot("Panel", _uiRoot);
            }

            if (_popupRoot == null)
            {
                _popupRoot = CreateRoot("Popup", _uiRoot);
            }
        }

        private Transform CreateRoot(string rootName, Transform parent)
        {
            Transform root = parent.Find(rootName);

            if (root != null)
            {
                return root;
            }

            GameObject rootObject = new GameObject(rootName);
            rootObject.transform.SetParent(parent);
            rootObject.transform.localPosition = Vector3.zero;
            rootObject.transform.localRotation = Quaternion.identity;
            rootObject.transform.localScale = Vector3.one;

            return rootObject.transform;
        }
    }
}
