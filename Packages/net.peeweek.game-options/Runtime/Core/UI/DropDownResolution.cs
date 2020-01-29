using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Dropdown))]
    public class DropDownResolution : MonoBehaviour
    {
        public DropDownRefreshRate dropDownRefreshRate;

        private void OnEnable()
        {
            var dropdown = GetComponent<Dropdown>();
            InitializeEntries();
            dropdown.onValueChanged.AddListener(UpdateOptions);
            UpdateOptions(dropdown.value);
        }

        private void OnDisable()
        {
            GetComponent<Dropdown>().onValueChanged.RemoveListener(UpdateOptions);
        }

        public void InitializeEntries()
        {
            var dropdown = GetComponent<Dropdown>();

            dropdown.options.Clear();
            int selected = 0;
            int i = 0;
            foreach (var res in Screen.resolutions.OrderByDescending(o => o.width))
            {
                string option = $"{res.width}x{res.height}";

                if (!dropdown.options.Any(o => o.text == option))
                {
                    dropdown.options.Add(new Dropdown.OptionData(option));
                    if (res.width == GameOption.Get<GraphicOption>().width && res.height == GameOption.Get<GraphicOption>().height)
                        selected = i;
                    i++;
                }

            }

            dropdown.SetValueWithoutNotify(selected);

            if (dropDownRefreshRate != null)
            {
                dropDownRefreshRate.InitializeEntries();
            }

        }

        void UpdateOptions(int value)
        {
            string option = GetComponent<Dropdown>().options[value].text;
            string[] values = option.Split('x');
            GameOption.Get<GraphicOption>().width = int.Parse(values[0]);
            GameOption.Get<GraphicOption>().height = int.Parse(values[1]);
        }
    }

}

