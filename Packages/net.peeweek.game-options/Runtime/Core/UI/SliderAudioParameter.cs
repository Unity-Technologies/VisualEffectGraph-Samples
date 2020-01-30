using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Slider))]
    public class SliderAudioParameter : MonoBehaviour
    {
        [Header("Audio Paramter")]
        public string Parameter = "Master";
        public Vector2 MinMaxParameterValue = new Vector2(-80, 0);
        public bool ApplyParameter = true;

        [Header("Slider")]
        public Vector2 MinMaxSliderValue = new Vector2(0, 1);
        [Tooltip("Logarithmic Scale (For Volume)")]
        public bool LogarithmicScale = false;
        public bool IntegerSliderValues = false;

        private void OnEnable()
        {
            var slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(UpdateOptions);

            if (LogarithmicScale)
            {
                slider.minValue = 0.0f;
                slider.maxValue = 1.0f;
            }
            else
            {
                slider.minValue = MinMaxSliderValue.x;
                slider.maxValue = MinMaxSliderValue.y;
            }

            slider.wholeNumbers = IntegerSliderValues;
            slider.SetValueWithoutNotify(ParameterToSlider(GameOption.Get<AudioOption>().GetParameter(Parameter)));
        }

        private void OnDisable()
        {
            GetComponent<Slider>().onValueChanged.RemoveListener(UpdateOptions);
        }

        void UpdateOptions(float value)
        {
            GameOption.Get<AudioOption>().SetParameter(Parameter,SliderToParameter(value), ApplyParameter);
        }

        float ParameterToSlider(float value)
        {
            if(LogarithmicScale)
            {
                return Mathf.Pow(10, value/20);
            }
            else
            {
                value = Mathf.InverseLerp(MinMaxParameterValue.x, MinMaxParameterValue.y, value);
                value = Mathf.Lerp(MinMaxSliderValue.x, MinMaxSliderValue.y, value);
                return value;
            }
        }

        float SliderToParameter(float value)
        {
            if (LogarithmicScale)
            {
                return Mathf.Log10(Mathf.Max(0.0001f,value)) * 20;
            }
            else
            {
                value = Mathf.InverseLerp(MinMaxSliderValue.x, MinMaxSliderValue.y, value);
                value = Mathf.Lerp(MinMaxParameterValue.x, MinMaxParameterValue.y, value);
                return value;
            }
        }

    }

}

