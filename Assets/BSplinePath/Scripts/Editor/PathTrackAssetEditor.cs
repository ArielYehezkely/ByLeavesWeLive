using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

namespace Mewlist.BSplinePath
{
    [CustomEditor(typeof(PathTrackAsset))]
    public class PathTrackAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var track = target as PathTrackAsset;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ControlRotation"));
            if (track.ControlRotation) EditorGUILayout.PropertyField(serializedObject.FindProperty("HorizontalRatation"));
            if (track.ControlRotation) EditorGUILayout.PropertyField(serializedObject.FindProperty("RotationSmoothing"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
