using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleVSync : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Toggle>().onValueChanged.AddListener(UpdateOptions);
        }

        private void OnDisable()
        {
            GetComponent<Toggle>().onValueChanged.RemoveListener(UpdateOptions);
        }

        public void InitializeEntries()
        {
            GetComponent<Toggle>().isOn = GameOptions.graphics.vSync;
        }

        void UpdateOptions(bool value)
        {
            GameOptions.graphics.vSync = value;
        }
    }

}

