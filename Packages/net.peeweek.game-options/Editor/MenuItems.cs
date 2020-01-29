using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameOptionsUtility
{
    internal static class MenuItems
    {
        [MenuItem("Assets/Create/Default Graphics Options")]
        internal static void CreateDefaultGraphicsOptions()
        {
            System.IO.Directory.CreateDirectory($"{Application.dataPath}/Resources");
            var asset = ScriptableObject.CreateInstance<GraphicOption>();
            AssetDatabase.CreateAsset(asset, $"Assets/Resources/{nameof(GraphicOption)}.asset");
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }


        [MenuItem("Assets/Create/Default Audio Options")]
        internal static void CreateDefaultAudioOptions()
        {

            System.IO.Directory.CreateDirectory($"{Application.dataPath}/Resources");
            var asset = ScriptableObject.CreateInstance<AudioOption>();
            AssetDatabase.CreateAsset(asset, $"Assets/Resources/{nameof(AudioOption)}.asset");
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }
    }
}


