using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Dropdown))]
    public class DropDownRefreshRate : MonoBehaviour
    {
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
            foreach (var res in Screen.resolutions)
            {
                if(res.width == GameOption.Get<GraphicOption>().width && res.height == GameOption.Get<GraphicOption>().height)
                {
                    if (!dropdown.options.Any(o => o.text == res.refreshRate.ToString()))
                        dropdown.options.Add(new Dropdown.OptionData(res.refreshRate.ToString()));

                    if (GameOption.Get<GraphicOption>().refreshRate == res.refreshRate)
                        selected = i;

                    i++;
                }
            }
            dropdown.SetValueWithoutNotify(selected);
        }

        void UpdateOptions(int value)
        {
            GameOption.Get<GraphicOption>().refreshRate = int.Parse(GetComponent<Dropdown>().options[value].text);
        }
    }

}

