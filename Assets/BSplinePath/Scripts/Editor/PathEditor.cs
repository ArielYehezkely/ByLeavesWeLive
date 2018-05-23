using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Mewlist.BSplinePath
{
    [CustomEditor(typeof(Path))]
    public class PathEditor : Editor
    {
        private static int? selected = null;

        private ReorderableList list;

        void OnSceneGUI()
        {
            EditPoints();
            DrawAsCurves();
        }

        void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("Points"));
            list.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
            {
                SerializedProperty property = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, property, GUIContent.none);
            };
            list.onSelectCallback = _ => selected = list.index;
            list.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Control points");
        }

        void EditPoints()
        {
            var path = target as Path;
            var transform = path.transform;
            for (int i = 0; i < path.Points.Count; i++)
            {
                if (Handles.Button(path.Points[i] + transform.localPosition, Quaternion.identity, 0.1f, selected.HasValue ? 0.2f : 0.5f, Handles.SphereHandleCap))
                {
                    if (selected == i) selected = null;
                    else selected = i;
                }
                if (selected.HasValue && selected.Value >= path.Points.Count) selected = null;

                if (selected.HasValue && selected.Value == i)
                {
                    EditorGUI.BeginChangeCheck();
                    path.Points[i] = Handles.DoPositionHandle(path.Points[i] + transform.localPosition, Quaternion.identity) - transform.localPosition;
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(path, "Point Moved");
                        path.ClearCache();
                    }
                }
            }
        }

        void DrawAsCurves()
        {
            var path = target as Path;
            var transform = path.transform;
            var resolution = path.Points.Count * 10;
            var colors = new Color[] { new Color(0f, 0f, 0f), new Color(1f, 1f, 1f) };
            for (int i = 0; i < resolution; i++)
            {
                Handles.color = colors[i % 2];
                Handles.DrawLine(path.Lerp((float)(i) / resolution), path.Lerp((float)(i + 1) / resolution));
            }
        }

        public override void OnInspectorGUI()
        {
            var path = target as Path;
            path.Rate = EditorGUILayout.Slider(path.Rate, 0f, 1f);
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
