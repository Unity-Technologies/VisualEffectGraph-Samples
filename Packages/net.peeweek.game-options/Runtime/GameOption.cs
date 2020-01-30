using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameOptionsUtility
{
    public abstract class GameOption : ScriptableObject
    {
        public static List<GameOption> all { get { return s_GameOptions.Values.ToList(); } }

        static Dictionary<Type, GameOption> s_GameOptions;

        public static T Get<T>() where T : GameOption
        {
            if (s_GameOptions == null)
                s_GameOptions = new Dictionary<Type, GameOption>();

            Type t = typeof(T);
            if (!s_GameOptions.ContainsKey(t))
                Add(t);

            return (T)s_GameOptions[t];


        }
        internal static void Add(Type t)
        {
            if (t.IsSubclassOf(typeof(GameOption)) && !t.IsAbstract)
            {
                if (s_GameOptions == null)
                    s_GameOptions = new Dictionary<Type, GameOption>();

                if (!s_GameOptions.ContainsKey(t))
                    s_GameOptions.Add(t, LoadOrDefault(t));
            }
        }

        static GameOption LoadOrDefault(Type t)
        {
            GameOption option = Resources.Load<GameOption>(t.Name);

            if (option == null)
            {
                option = (GameOption)CreateInstance(t);
            }

            return option;
        }

        public abstract void Apply();
    }
}

