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


        private void Awake()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        public void Initialize(string tagID, string labelText)
        {
            TagID = tagID;
            _labelText.text = labelText;
        }

        private void OnValueChanged(bool isOn)
        {
            _parent.OnClickTagToggle(this, isOn);
        }
    }
}
