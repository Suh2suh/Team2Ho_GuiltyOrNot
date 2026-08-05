using System;
using System.Collections.Generic;
using UnityEngine;

namespace Judge
{
    [Serializable]
    public class UIData
    {
        public UIList UIName;
        public GameObject Prefab;
    }

    [CreateAssetMenu(fileName = "UIDatabase", menuName = "Judge/UI Database")]
    public class UIDatabase : ScriptableObject
    {
        [SerializeField] private List<UIData> _uiDataList = new List<UIData>();

        public IReadOnlyList<UIData> UIDataList => _uiDataList;
    }
}
