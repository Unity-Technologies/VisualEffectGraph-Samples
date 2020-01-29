#if GAME_OPTIONS_HDRP
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace GameOptionsUtility.HDRP
{
    public class HDRPCameraOption : GameOption
    {
        public class Preferences
        {
            public const string prefix = GameOptions.Preferences.prefix + "HDRP.";
            public const string antialiasing = prefix + "AntiAliasing";
        }

        static List<HDAdditionalCameraData> s_Cameras;

        public HDAdditionalCameraData.AntialiasingMode antiAliasing
        {
            get { return (HDAdditionalCameraData.AntialiasingMode)PlayerPrefs.GetInt(Preferences.antialiasing, (int)HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing); }
            set { PlayerPrefs.SetInt(Preferences.antialiasing, (int)value); }
        }


        public static void AddCamera(HDAdditionalCameraData camera)
        {
            if (s_Cameras == null)
                s_Cameras = new List<HDAdditionalCameraData>();

            if (!s_Cameras.Contains(camera))
                s_Cameras.Add(camera);

            Get<HDRPCameraOption>().Apply();
        }

        public static void RemoveCamera(HDAdditionalCameraData camera)
        {
            if (s_Cameras == null)
                s_Cameras = new List<HDAdditionalCameraData>();

            if (s_Cameras.Contains(camera))
                s_Cameras.Remove(camera);
        }

        public override void Apply()
        {
            if (s_Cameras == null)
                return;

            foreach (var camera in s_Cameras)
            {
                camera.antialiasing = antiAliasing;
            }
        }
    }
}
#endif