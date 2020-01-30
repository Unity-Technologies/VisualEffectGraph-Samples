using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameOptionsUtility
{
    public class GameOptions : MonoBehaviour
    {
        public class Preferences
        {
            public const string prefix = "GameOptions.";
        }

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            var gameObject = new GameObject("Game Options");
            gameObject.AddComponent<GameOptions>();
            DontDestroyOnLoad(gameObject);
            Load();
        }

        private void Start()
        {
            Apply();
        }

        static void Load()
        {
            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach(var type in assembly.GetTypes())
                    {
                        if(type.IsSubclassOf(typeof(GameOption)) && !type.IsAbstract)
                        {
                            GameOption.Add(type);
                        }
                    }
                }
                catch
                {
                    Debug.LogWarning($"Could not load any game option from assembly {assembly.FullName}");
                }
            }
        }

        public static void Apply()
        {
            foreach(var gameOption in GameOption.all)
            {
                gameOption.Apply();
            }
        }
    }
}

