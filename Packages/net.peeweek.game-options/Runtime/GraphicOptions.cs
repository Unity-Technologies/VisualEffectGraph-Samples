using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOptionsUtility
{
    public class GraphicOptions : ScriptableObject
    {
        public class Preferences
        {
            public const string prefix = GameOptions.Preferences.prefix + "Graphics.";
            public const string fullScreenMode = prefix + "FullScreenMode";
            public const string vSync = prefix + "VSync";
            public const string targetFrameRate = prefix + "TargetFrameRate";
            public const string resolutionWidth = prefix + "ResolutionWidth";
            public const string resolutionHeight = prefix + "ResolutionHeight";
            public const string refreshRate = prefix + "RefreshRate";
        }

        public FullScreenMode fullScreenMode
        {
            get { return(FullScreenMode)PlayerPrefs.GetInt(Preferences.fullScreenMode, (int)m_DefaultFullScreenMode); }
            set { PlayerPrefs.SetInt(Preferences.fullScreenMode, (int)value); }
        }

        public bool vSync
        {
            get { return PlayerPrefs.GetInt(Preferences.vSync, m_DefaultVSync? 1 : 0) == 1? true : false; }
            set { PlayerPrefs.SetInt(Preferences.vSync, value? 1 : 0); }
        }

        public int targetFrameRate
        {
            get { return PlayerPrefs.GetInt(Preferences.targetFrameRate, m_DefaultTargetFrameRate); }
            set { PlayerPrefs.SetInt(Preferences.targetFrameRate, value); }
        }
        public int width
        {
            get { return PlayerPrefs.GetInt(Preferences.resolutionWidth, m_DefaultNativeResolution ? Screen.width : m_DefaultWidth); }
            set { PlayerPrefs.SetInt(Preferences.resolutionWidth, value); }
        }

        public int height
        {
            get { return PlayerPrefs.GetInt(Preferences.resolutionHeight, m_DefaultNativeResolution ? Screen.height : m_DefaultHeight); }
            set { PlayerPrefs.SetInt(Preferences.resolutionHeight, value); }
        }
        public int refreshRate
        {
            get { return PlayerPrefs.GetInt(Preferences.refreshRate, m_DefaultRefreshRate); }
            set { PlayerPrefs.SetInt(Preferences.refreshRate, value); }
        }

        [Header("Defaults")]
        [SerializeField]
        protected FullScreenMode m_DefaultFullScreenMode = FullScreenMode.FullScreenWindow;
        [SerializeField]
        protected bool m_DefaultVSync = true;
        [SerializeField]
        protected int m_DefaultTargetFrameRate = -1;
        [SerializeField]
        protected bool m_DefaultNativeResolution = true;
        [SerializeField]
        protected int m_DefaultWidth = 1280;
        [SerializeField]
        protected int m_DefaultHeight = 720;
        [SerializeField]
        protected int m_DefaultRefreshRate = 60;
        [SerializeField]
        protected int m_DefaultMonitor = 0;

        public static GraphicOptions Load()
        {
            var graphics = Resources.Load<GraphicOptions>(nameof(GraphicOptions));
            if (graphics == null)
            {
                graphics = CreateInstance<GraphicOptions>();
            }
            return graphics;
        }

        public void Apply()
        {
            Screen.SetResolution(width, height, fullScreenMode, refreshRate);
            QualitySettings.vSyncCount = vSync ? 1 : 0;
            Application.targetFrameRate = targetFrameRate;
        }
    }
}

