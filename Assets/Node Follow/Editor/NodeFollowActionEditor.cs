using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeFollowAction))]
public class NodeFollowActionEditor : Editor
{
    #region Reference to the script this custom editor was made for
    NodeFollowAction script;
    #endregion

    void OnEnable()
    {
        #region The target for custom editor
        script = (NodeFollowAction)target;
        #endregion
    }
    public override void OnInspectorGUI()
    {
        #region Method name "text"
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Method Name");
        GUILayout.FlexibleSpace();
        script.actionName = EditorGUILayout.TextField(script.actionName, GUILayout.MinWidth(120), GUILayout.MaxWidth(120), GUILayout.ExpandWidth(false));
        GUILayout.EndHorizontal();
        #endregion

        #region Node number "Int"
        GUILayout.BeginHorizontal();
        GUILayout.Label("Node Number");
        GUILayout.FlexibleSpace();
        script.node = EditorGUILayout.IntField(script.node, GUILayout.MinWidth(25), GUILayout.MaxWidth(25), GUILayout.ExpandWidth(false));
        GUILayout.EndHorizontal();
        #endregion

        #region Doing action "Bool"
        GUILayout.BeginHorizontal();
        GUILayout.Label("Doing Action");
        GUILayout.FlexibleSpace();
        EditorGUI.BeginDisabledGroup(true);
        script.doingAction = EditorGUILayout.Toggle(script.doingAction, GUILayout.MinWidth(25), GUILayout.MaxWidth(25), GUILayout.ExpandWidth(false));
        EditorGUI.EndDisabledGroup();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion

        #region Undo
        Undo.RecordObject(target, "Node Follow Action Changes");
        #endregion
    }
}