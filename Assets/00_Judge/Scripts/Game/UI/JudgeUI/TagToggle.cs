using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Judge
{
    public class TagToggle : MonoBehaviour
    {
        [SerializeField] private JudgeUI _parent;

        [SerializeField] private Toggle _toggle;
        [SerializeField] private TextMeshProUGUI _labelText;

        public Toggle Toggle => _toggle;
		public string TagID { get; private set; }


        public void Initialize(string tagID, string labelText)
        {
            TagID = tagID;
            _labelText.text = labelText;

            _toggle.onValueChanged.AddListener(NotiParentClicked);

            void NotiParentClicked(bool isOn)
            {
                _parent.OnClickTagToggle(this, isOn);
            }
		}
    }
}
