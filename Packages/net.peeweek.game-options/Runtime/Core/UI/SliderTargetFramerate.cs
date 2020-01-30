using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Slider))]
    public class SliderTargetFramerate : MonoBehaviour
    {
        public Vector2 MinMaxFramerate = new Vector2(15, 144);

        private void OnEnable()
        {
            var slider = GetComponent<Slider>();
            InitializeEntries(slider);
            slider.onValueChanged.AddListener(UpdateOptions);
            UpdateOptions(slider.value);
        }

        private void OnDisable()
        {
            GetComponent<Slider>().onValueChanged.RemoveListener(UpdateOptions);
        }

        void InitializeEntries(Slider slider)
        {
            slider.value = GameOption.Get<GraphicOption>().targetFrameRate;
        }

        void UpdateOptions(float value)
        {
            GameOption.Get<GraphicOption>().targetFrameRate = (int)value;
        }
    }

}

