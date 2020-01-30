#if GAME_OPTIONS_HDRP
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

namespace GameOptionsUtility.HDRP
{
    [RequireComponent(typeof(Dropdown))]
    public class HDRPAntiAliasingDropdown : MonoBehaviour
    {
        public bool ApplyImmediately = true;
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
            dropdown.options.Add(new Dropdown.OptionData("None"));
            dropdown.options.Add(new Dropdown.OptionData("FXAA"));
            dropdown.options.Add(new Dropdown.OptionData("TAA"));
            dropdown.options.Add(new Dropdown.OptionData("SMAA"));
            dropdown.SetValueWithoutNotify((int)GameOption.Get<HDRPCameraOption>().antiAliasing);
        }

        void UpdateOptions(int value)
        {
            var option = GameOption.Get<HDRPCameraOption>();
            option.antiAliasing = (HDAdditionalCameraData.AntialiasingMode)value;

            if (ApplyImmediately)
                option.Apply();
        }
    }
}
#endif



