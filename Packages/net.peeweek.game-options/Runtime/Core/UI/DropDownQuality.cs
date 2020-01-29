using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Dropdown))]
    public class DropDownQuality : MonoBehaviour
    {
        private void OnEnable()
        {
            var dropdown = GetComponent<Dropdown>();
            InitializeEntries(dropdown);
            dropdown.onValueChanged.AddListener(UpdateOptions);
            UpdateOptions(dropdown.value);
        }

        private void OnDisable()
        {
            GetComponent<Dropdown>().onValueChanged.RemoveListener(UpdateOptions);
        }

        public void InitializeEntries(Dropdown dropdown)
        {
            dropdown.options.Clear();

            foreach(var quality in QualitySettings.names)
                dropdown.options.Add(new Dropdown.OptionData(quality));

            int current = GameOption.Get<GraphicOption>().quality;
            dropdown.SetValueWithoutNotify(current);
        }

        void UpdateOptions(int value)
        {
            GameOption.Get<GraphicOption>().quality = value;
        }
    }

}

