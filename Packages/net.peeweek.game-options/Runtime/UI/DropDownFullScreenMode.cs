using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Dropdown))]
    public class DropDownFullScreenMode : MonoBehaviour
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
            dropdown.options.Add(new Dropdown.OptionData("Full Screen"));
            dropdown.options.Add(new Dropdown.OptionData("Full Screen (Windowed)"));
            dropdown.options.Add(new Dropdown.OptionData("Maximized Window"));
            dropdown.options.Add(new Dropdown.OptionData("Window"));
        }

        void UpdateOptions(int value)
        {
            GameOptions.graphics.fullScreenMode = (FullScreenMode)value;
        }
    }

}

