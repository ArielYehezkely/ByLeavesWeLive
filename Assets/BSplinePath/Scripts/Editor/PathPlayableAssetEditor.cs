using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

namespace Mewlist.BSplinePath
{
    [CustomEditor(typeof(PathPlayableAsset))]
    public class PathPlayableAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var asset = target as PathPlayableAsset;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Path"));
            EditorGUILayout.CurveField("Curve", asset.Curve, Color.blue, new Rect(0, 0, 1, 1));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
