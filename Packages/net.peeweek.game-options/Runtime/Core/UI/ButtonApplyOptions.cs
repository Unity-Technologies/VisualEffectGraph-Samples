using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOptionsUtility
{
    [RequireComponent(typeof(Button))]
    public class ButtonApplyOptions : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(Apply);
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(Apply);
        }

        void Apply()
        {
            GameOptions.Apply();
        }
    }
}
