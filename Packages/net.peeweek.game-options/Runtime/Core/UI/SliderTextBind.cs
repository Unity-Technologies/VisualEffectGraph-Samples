using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility 
{
    [RequireComponent(typeof(Slider))]
    public class SliderTextBind : MonoBehaviour
    {
        [Tooltip("Text UI")]
        public Text text;
        [Tooltip("Text Prefix")]
        public string Prefix = "";
        [Tooltip("Text Suffix")]
        public string Suffix = "";
        [Tooltip("Multiplies value by 100")]
        public bool AsPercentage = false;
        [Tooltip("Numeric format as seen in C# Standard Numeric Format Strings")]
        public string NumberFormat = "F2";

        private void OnEnable()
        {
            var slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(UpdateText);
            UpdateText(slider.value);
        }

        private void OnDisable()
        {
            GetComponent<Slider>().onValueChanged.RemoveListener(UpdateText);
        }

        void UpdateText(float value)
        {
            value = AsPercentage ? value * 100 : value;

            text.text = $"{Prefix}{value.ToString(NumberFormat)}{Suffix}";
        }
    }

}

