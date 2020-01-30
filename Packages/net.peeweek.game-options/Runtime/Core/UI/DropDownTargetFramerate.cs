using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Dropdown))]
    public class DropDownTargetFramerate : MonoBehaviour
    {
        public int[] TargetFramerates = new int[4] { -1, 15, 30, 60 };
        public string InfiniteText = "Infinite";
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
            foreach(var framerate in TargetFramerates)
            {
                dropdown.options.Add(new Dropdown.OptionData(framerate == -1? InfiniteText : framerate.ToString()));
            }

            int current = GameOption.Get<GraphicOption>().targetFrameRate;
            dropdown.SetValueWithoutNotify(dropdown.options.FindIndex(o => o.text == current.ToString()));
        }

        void UpdateOptions(int value)
        {
            GameOption.Get<GraphicOption>().targetFrameRate = TargetFramerates[value];
        }
    }

}

