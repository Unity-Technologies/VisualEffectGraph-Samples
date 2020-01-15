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
            slider.onValueChanged.AddListener(UpdateOptions);
            UpdateOptions(slider.value);
        }

        private void OnDisable()
        {
            GetComponent<Slider>().onValueChanged.RemoveListener(UpdateOptions);
        }

        void UpdateOptions(float value)
        {
            GameOptions.graphics.targetFrameRate = (int)value;
        }
    }

}

