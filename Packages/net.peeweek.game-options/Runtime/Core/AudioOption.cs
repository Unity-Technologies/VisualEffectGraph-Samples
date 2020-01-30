using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GameOptionsUtility
{
    internal class AudioOption : GameOption
    {
        public class Preferences
        {
            public const string prefix = GameOptions.Preferences.prefix + "Audio.";
            public const string parameterPrefix = prefix + "Parameter.";
        }

        public float GetParameter(string paramName)
        {
            if (m_Parameters.Any(o => o.Parameter == paramName))
            {
                var param = m_Parameters.First(o => o.Parameter == paramName);
                return PlayerPrefs.GetFloat(Preferences.parameterPrefix + paramName, param.Value);
            }
            else
                return 0;
        }

        public void SetParameter(string paramName, float value, bool Apply)
        {
            if (m_Parameters.Any(o => o.Parameter == paramName))
            {
                PlayerPrefs.SetFloat(Preferences.parameterPrefix + paramName, value);

                if(Apply)
                    SetParameter(paramName, value);

            }
        }

        public AudioMixer mixer
        {
            get
            {
                AudioMixer mixer = m_AudioMixer;

                if (mixer == null)
                    mixer = Object.FindObjectOfType<AudioMixer>();

                if (mixer == null)
                {
                    Debug.LogWarning("No Mixer has been found in the project");
                    return null;
                }
                return mixer;
            }
        }

        [Header("Audio Mixer"), SerializeField]
        protected AudioMixer m_AudioMixer;

        [Header("Parameters"), SerializeField]
        protected ParameterValue[] m_Parameters = new ParameterValue[0];

        [System.Serializable]
        public struct ParameterValue
        {
            public string Parameter;
            public float Value;
        }

        public override void Apply()
        {
            foreach(var parameter in m_Parameters)
            {
                SetParameter(parameter.Parameter, GetParameter(parameter.Parameter));
            }
        }

        void SetParameter(string parameter, float value)
        {
            AudioMixer currentMixer = this.mixer;

            if(currentMixer != null)
                currentMixer.SetFloat(parameter, value);

        }
    }

}
