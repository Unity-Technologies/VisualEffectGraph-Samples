using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOptionsUtility
{
    public static class GameOptions
    {
        public class Preferences
        {
            public const string prefix = "GameOptions.";
        }

        public static GraphicOptions graphics;
        public static AudioOptions audio;

        static GameOptions()
        {
            graphics = GraphicOptions.Load();
            graphics.Apply();
            audio = AudioOptions.Load();
            audio.Apply();
        }
    }
}

