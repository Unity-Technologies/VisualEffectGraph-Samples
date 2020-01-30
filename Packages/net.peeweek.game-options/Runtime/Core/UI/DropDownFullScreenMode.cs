using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Dropdown))]
    public class DropDownFullScreenMode : MonoBehaviour
    {
        public Dropdown refreshRateDropdown;

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
            dropdown.options.Add(new Dropdown.OptionData("Full Screen (Exclusive)"));
            dropdown.options.Add(new Dropdown.OptionData("Full Screen (Windowed)"));
            dropdown.options.Add(new Dropdown.OptionData("Maximized Window"));
            dropdown.options.Add(new Dropdown.OptionData("Window"));
            dropdown.SetValueWithoutNotify((int)GameOption.Get<GraphicOption>().fullScreenMode);
        }

        void UpdateOptions(int value)
        {
            GameOption.Get<GraphicOption>().fullScreenMode = (FullScreenMode)value;
            if (refreshRateDropdown != null)
            {
                refreshRateDropdown.interactable = (value == 0);
                refreshRateDropdown.captionText.CrossFadeAlpha(value > 0 ? 0.1f : 1.0f, refreshRateDropdown.colors.fadeDuration, true);
            }
        }
    }

}

