using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameOptionsUtility.Editor
{
    static class MenuItems
    {
        [MenuItem("Assets/Create/Default Graphics Options")]
        static void CreateDefaultGraphicsOptions()
        {

            System.IO.Directory.CreateDirectory($"{Application.dataPath}/Resources");
            var asset = ScriptableObject.CreateInstance<GraphicOptions>();
            AssetDatabase.CreateAsset(asset, $"Assets/Resources/{nameof(GraphicOptions)}.asset");
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }


        [MenuItem("Assets/Create/Default Audio Options")]
        static void CreateDefaultAudioOptions()
        {

            System.IO.Directory.CreateDirectory($"{Application.dataPath}/Resources");
            var asset = ScriptableObject.CreateInstance<AudioOptions>();
            AssetDatabase.CreateAsset(asset, $"Assets/Resources/{nameof(AudioOptions)}.asset");
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }
    }
}


