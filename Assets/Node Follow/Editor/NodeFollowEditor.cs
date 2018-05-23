using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeFollow))]
public class NodeFollowEditor : Editor
{
    #region Reference to the script this custom editor was made for
    NodeFollow script;
    #endregion

    #region Reference to the EnemyAI Image
    private Texture2D nodeFollowLabel;
    #endregion

    void OnEnable()
    {
        #region The target for custom editor
        script = (NodeFollow)target;
        #endregion

        #region Node Follow Label Image
        nodeFollowLabel = Resources.Load("NodeFollow Label") as Texture2D;
        #endregion
    }

    public override void OnInspectorGUI()
    {
        #region Update serialized object
        serializedObject.Update();
        #endregion

        #region Font size "GUIStyle"
        GUIStyle fontSize = new GUIStyle();
        fontSize.normal.textColor = new Color32(0, 0, 0, 255);
        fontSize.fontStyle = FontStyle.Bold;
        fontSize.fontSize = 12;
        #endregion

        #region Background Box
        GUI.backgroundColor = script.backgroundColor;
        GUILayout.BeginVertical("Box");
        #endregion

        #region Moving object settings "Foldout"
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Moving object settings", fontSize);
        GUILayout.Space(10);
        script.objectSettingsFoldout = EditorGUILayout.Foldout(script.objectSettingsFoldout, GUIContent.none);
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.white;
        GUI.backgroundColor = script.backgroundColor;
        GUILayout.EndHorizontal();
        #endregion

        #region Moving object "Foldout"
        if (script.objectSettingsFoldout == true)
        {
            #region Moving object "Gameobject"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Moving Object");
            GUILayout.FlexibleSpace();
            script.movingObject = (GameObject)EditorGUILayout.ObjectField(script.movingObject, typeof(GameObject), true, GUILayout.MinWidth(120), GUILayout.MaxWidth(120), GUILayout.ExpandWidth(false));
            GUILayout.Space(6);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Movement Speed "Float"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Movement Speed | Node Based?");
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            script.movementSpeed = EditorGUILayout.FloatField(script.movementSpeed, GUILayout.MinWidth(102), GUILayout.MaxWidth(102), GUILayout.ExpandWidth(false));
            script.nodeBasedSpeed = EditorGUILayout.Toggle(script.nodeBasedSpeed, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            if (EditorGUI.EndChangeCheck() && script.nodes != null)
            {
                for (int i = 0; i < script.speeds.Length; i++)
                {
                    script.speeds[i] = script.movementSpeed;
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Rotation speed "Float" 
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Rotation Speed | Node Based?");
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            script.rotationSpeed = EditorGUILayout.FloatField(script.rotationSpeed, GUILayout.MinWidth(102), GUILayout.MaxWidth(102), GUILayout.ExpandWidth(false));
            script.nodeBasedRotation = EditorGUILayout.Toggle(script.nodeBasedRotation, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            if (EditorGUI.EndChangeCheck() && script.nodes != null)
            {
                for (int i = 0; i < script.rotations.Length; i++)
                {
                    script.rotations[i] = script.rotationSpeed;
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion
        }
        GUILayout.EndVertical();
        #endregion

        #region Space
        GUILayout.Space(2);
        #endregion

        #region Movement settings "Foldout"
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Movement settings", fontSize);
        GUILayout.Space(10);
        script.movementSettingsFoldout = EditorGUILayout.Foldout(script.movementSettingsFoldout, GUIContent.none);
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.white;
        GUI.backgroundColor = script.backgroundColor;
        GUILayout.EndHorizontal();
        #endregion

        #region Movement settings "Foldout"
        if (script.movementSettingsFoldout == true)
        {
            #region Use trigger "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            if (script.useTrigger == false)
            {
                GUILayout.Label("Use Trigger");
            }
            if (script.useTrigger == true)
            {
                GUILayout.Label("Use Trigger | What Can Trigger?");
            }
            GUILayout.FlexibleSpace();
            if (script.useTrigger == true)
            {
                script.triggerName = EditorGUILayout.TextField(script.triggerName, GUILayout.MinWidth(95), GUILayout.MaxWidth(95), GUILayout.ExpandWidth(false));
            }
            if (script.useTrigger == false)
            {
                script.triggerName = "";
            }
            script.useTrigger = EditorGUILayout.Toggle(script.useTrigger, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Move to start at end "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Move To Start At End");
            GUILayout.FlexibleSpace();
            script.moveToStart = EditorGUILayout.Toggle(script.moveToStart, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Loop movement "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Loop Movement");
            GUILayout.FlexibleSpace();
            script.loop = EditorGUILayout.Toggle(script.loop, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Rotate towards next node "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Rotate Towards Next Node");
            GUILayout.FlexibleSpace();
            script.rotateTowardsNextNode = EditorGUILayout.Toggle(script.rotateTowardsNextNode, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Moving object direction "Enum"
            if (script.rotateTowardsNextNode == true)
            {
                GUI.backgroundColor = Color.white;
                GUILayout.BeginHorizontal();
                GUILayout.Label("Moving Object Direction");
                GUILayout.FlexibleSpace();
                script.objectDirection = (NodeFollow.Direction)EditorGUILayout.EnumPopup(script.objectDirection, GUILayout.MinWidth(120), GUILayout.MaxWidth(120), GUILayout.ExpandWidth(false));
                GUILayout.Space(6);
                GUILayout.EndHorizontal();
                GUI.backgroundColor = script.backgroundColor;
            }
            #endregion
        }
        GUILayout.EndVertical();
        #endregion

        #region Space
        GUILayout.Space(2);
        #endregion

        #region Visual settings "Foldout"
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Visual settings", fontSize);
        GUILayout.Space(10);
        script.visualSettingsFoldout = EditorGUILayout.Foldout(script.visualSettingsFoldout, GUIContent.none);
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.white;
        GUI.backgroundColor = script.backgroundColor;
        GUILayout.EndHorizontal();
        #endregion

        #region Visual settings "Foldout"
        if (script.visualSettingsFoldout == true)
        {
            #region Draw lines "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Draw Lines");
            GUILayout.FlexibleSpace();
            script.drawLines = EditorGUILayout.Toggle(script.drawLines, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Draw dot lines "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Draw Dot Lines");
            GUILayout.FlexibleSpace();
            script.drawDotLine = EditorGUILayout.Toggle(script.drawDotLine, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Draw line from last to first "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Draw Line From Last To First");
            GUILayout.FlexibleSpace();
            script.drawLastToFirst = EditorGUILayout.Toggle(script.drawLastToFirst, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Show handles "Bool"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Handles");
            GUILayout.FlexibleSpace();
            script.showHandles = EditorGUILayout.Toggle(script.showHandles, GUILayout.MinWidth(20), GUILayout.MaxWidth(20), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Handles size "Slider"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Handles Size");
            GUILayout.FlexibleSpace();
            script.handlesSize = EditorGUILayout.Slider(script.handlesSize, 0.1f, 5f, GUILayout.MinWidth(200), GUILayout.MaxWidth(200), GUILayout.ExpandWidth(false));
            GUILayout.Space(6);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Line color "Enum"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Line Color");
            GUILayout.FlexibleSpace();
            script.HandlesColor = (NodeFollow.SetHandlesColor)EditorGUILayout.EnumPopup(script.HandlesColor, GUILayout.MinWidth(200), GUILayout.MaxWidth(200), GUILayout.ExpandWidth(false));
            GUILayout.Space(6);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion

            #region Background color "Color"
            GUI.backgroundColor = Color.white;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Background color");
            GUILayout.FlexibleSpace();
            script.backgroundColor = EditorGUILayout.ColorField(script.backgroundColor, GUILayout.MinWidth(50), GUILayout.MaxWidth(50), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.backgroundColor = script.backgroundColor;
            #endregion
        }
        GUILayout.EndVertical();
        #endregion

        #region Space
        GUILayout.Space(2);
        #endregion

        #region Store old label width reference
        var oldWidth = EditorGUIUtility.labelWidth;
        #endregion

        #region Use new label width
        EditorGUIUtility.labelWidth = 12;
        #endregion

        #region Offsets
        GUILayout.BeginVertical("Box");
        GUILayout.BeginVertical("Box");
        GUILayout.Space(2);
        GUI.backgroundColor = Color.white;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Offset");
        script.xResetPosition = EditorGUILayout.FloatField("X", script.xResetPosition, GUILayout.MinWidth(50), GUILayout.MaxWidth(50), GUILayout.ExpandWidth(false));
        script.yResetPosition = EditorGUILayout.FloatField("Y", script.yResetPosition, GUILayout.MinWidth(50), GUILayout.MaxWidth(50), GUILayout.ExpandWidth(false));
        script.zResetPosition = EditorGUILayout.FloatField("Z", script.zResetPosition, GUILayout.MinWidth(50), GUILayout.MaxWidth(50), GUILayout.ExpandWidth(false));
        GUILayout.FlexibleSpace();

        #region Reset positions "Button"
        if (GUILayout.Button("Reset positions", EditorStyles.toolbarButton, GUILayout.Width(100)) && script.nodes.Length != 0)
        {
            for (int i = 0; i < script.nodes.Length; i++)
            {
                if (script.nodes.Length == 1)
                {
                    script.nodes[i].Set(script.transform.position.x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                }
                if (script.nodes.Length != 1)
                {
                    if (i < script.nodes.Length)
                    {
                        if (i == 0)
                        {
                            script.nodes[i].Set(script.transform.position.x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                        }
                        if (i >= 1 && i < script.nodes.Length - 1)
                        {
                            script.nodes[i].Set(script.nodes[i - 1].x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                        }
                        if (i == script.nodes.Length - 1)
                        {
                            script.nodes[i].Set(script.nodes[i - 1].x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                        }
                    }
                }
            }
        }
        #endregion

        GUILayout.Space(2);
        GUILayout.EndHorizontal();
        GUILayout.Space(2);
        GUILayout.EndVertical();
        GUI.backgroundColor = script.backgroundColor;
        #endregion

        #region Use old label width
        EditorGUIUtility.labelWidth = oldWidth;
        #endregion

        #region Nodes, Stops, Speeds and Rotations lists
        GUI.backgroundColor = Color.white;
        EditorList.Show(serializedObject.FindProperty("nodes"), true, true, "Node ", true, true);
        EditorList.Show(serializedObject.FindProperty("stops"), false, true, "Node ", true, false);
        if (script.nodes.Length <= 0)
        {
            serializedObject.FindProperty("stops").isExpanded = false;
        }
        if (script.nodeBasedSpeed == true)
        {
            EditorList.Show(serializedObject.FindProperty("speeds"), false, true, "Node ", true, false);
        }
        if (script.nodeBasedSpeed == false || script.nodes.Length <= 0)
        {
            serializedObject.FindProperty("speeds").isExpanded = false;
        }
        if (script.nodeBasedRotation == true)
        {
            EditorList.Show(serializedObject.FindProperty("rotations"), false, true, "Node ", true, false);
        }
        if (script.nodeBasedRotation == false || script.nodes.Length <= 0)
        {
            serializedObject.FindProperty("rotations").isExpanded = false;
        }
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        #endregion

        #region Apply modifications to serialized object
        serializedObject.ApplyModifiedProperties();
        #endregion

        #region Undo
        Undo.RecordObject(target, "Node Follow Changes");
        #endregion
    }

    void OnSceneGUI()
    {       
        #region Updating all node positions in case transform is moved
        if (script.nodes != null)
        {
            script.currentPosition = script.transform.position;

            if (script.currentPosition != script.lastPosition)
            {
                for (int i = 0; i < script.nodes.Length; i++)
                {
                    var differenceX = script.lastPosition.x - script.currentPosition.x;
                    var differenceY = script.lastPosition.y - script.currentPosition.y;
                    var differenceZ = script.lastPosition.z - script.currentPosition.z;

                    script.newX = script.nodes[i].x - differenceX;
                    script.newY = script.nodes[i].y - differenceY;
                    script.newZ = script.nodes[i].z - differenceZ;

                    script.nodes[i] = new Vector3(script.newX, script.newY, script.nodes[i].z);
                }

                script.lastPosition = script.currentPosition;
            }
        }
        #endregion

        #region Setting handles color and switching gizmo icon color to match handles color
        switch (script.HandlesColor)
        {
            case NodeFollow.SetHandlesColor.white:
                Handles.color = Color.white;
                script.useWhiteGizmo = true;
                script.useBlueGizmo = false;
                script.useRedGizmo = false;
                script.useGreenGizmo = false;
                script.useYellowGizmo = false;
                break;
            case NodeFollow.SetHandlesColor.blue:
                Handles.color = Color.blue;
                script.useWhiteGizmo = false;
                script.useBlueGizmo = true;
                script.useRedGizmo = false;
                script.useGreenGizmo = false;
                script.useYellowGizmo = false;
                break;
            case NodeFollow.SetHandlesColor.red:
                Handles.color = Color.red;
                script.useWhiteGizmo = false;
                script.useBlueGizmo = false;
                script.useRedGizmo = true;
                script.useGreenGizmo = false;
                script.useYellowGizmo = false;
                break;
            case NodeFollow.SetHandlesColor.green:
                Handles.color = Color.green;
                script.useWhiteGizmo = false;
                script.useBlueGizmo = false;
                script.useRedGizmo = false;
                script.useGreenGizmo = true;
                script.useYellowGizmo = false;
                break;
            case NodeFollow.SetHandlesColor.yellow:
                Handles.color = Color.yellow;
                script.useWhiteGizmo = false;
                script.useBlueGizmo = false;
                script.useRedGizmo = false;
                script.useGreenGizmo = false;
                script.useYellowGizmo = true;
                break;
        }
        #endregion

        #region Show handles
        if (script.showHandles == true && script.nodes != null)
        {
            if (script.firstNodesplaced == false)
            {
                for (int i = 0; i < script.nodes.Length; i++)
                {
                    if (script.nodes.Length == 1)
                    {
                        script.nodes[i].Set(script.transform.position.x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                    }
                    if (script.nodes.Length != 1)
                    {
                        if (i < script.nodes.Length)
                        {
                            if (i == 0)
                            {
                                script.nodes[i].Set(script.transform.position.x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                            }
                            if (i >= 1 && i < script.nodes.Length - 1)
                            {
                                script.nodes[i].Set(script.nodes[i - 1].x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                            }
                            if (i == script.nodes.Length - 1)
                            {
                                script.nodes[i].Set(script.nodes[i - 1].x + script.xResetPosition, script.transform.position.y + script.yResetPosition, script.transform.position.z + script.zResetPosition);
                            }
                        }
                    }
                }             
            }
            if (script.firstNodesplaced == true)
            {
                for (int i = 0; i < script.nodes.Length; i++)
                {
                    script.nodes[i] = Handles.FreeMoveHandle(script.nodes[i], Quaternion.identity, script.handlesSize, new Vector3(0.05f, 0.05f, 0), Handles.CircleHandleCap);
                }
            }
            if (script.nodes.Length == 0)
            {
                script.firstNodesplaced = false;
            }
            if (script.nodes.Length != 0)
            {
                script.firstNodesplaced = true;
            }
        }
        #endregion

        #region Check if lines should be drawn
        if (script.drawLines == false || script.nodes == null)
        {
            return;
        }
        #endregion

        #region Label style
        var labelStyle = new GUIStyle();

        labelStyle.normal.background = nodeFollowLabel;
        labelStyle.fontSize = 12;
        labelStyle.normal.textColor = Color.white;
        labelStyle.contentOffset = new Vector2(2, 0);

        Vector3 labelOffset = new Vector3(0, 1.75f);
        #endregion

        #region Labels
        if (script.nodes.Length == 1)
        {
            Handles.Label(script.nodes[0] + labelOffset, " Node " + 1 + "  ", labelStyle);
        }
        else
        {
            for (int i = 0; i < script.nodes.Length - 1; i++)
            {
                Handles.Label(script.nodes[i] + labelOffset, " Node " + (i + 1) + "  ", labelStyle);
                Handles.Label(script.nodes[i + 1] + labelOffset, " Node " + (i + 2) + "  ", labelStyle);
            }
        }
        #endregion

        #region Line type selection
        if (script.drawDotLine == true)
        {
            for (int i = 0; i < script.nodes.Length - 1; i++)
            {
                Handles.DrawDottedLine(script.nodes[i], script.nodes[(int)Mathf.Repeat(i + 1, script.nodes.Length)], 20f);
            }
        }
        else
        {
            for (int i = 0; i < script.nodes.Length - 1; i++)
            {
                Handles.DrawLine(script.nodes[i], script.nodes[(int)Mathf.Repeat(i + 1, script.nodes.Length)]);
            }
        }
        #endregion

        #region Draw line from last node to the first
        if (script.drawDotLine == true)
        {
            if (script.drawLastToFirst == true && script.nodes.Length != 0)
            {
                for (int i = 0; i < script.nodes.Length - 1; i++)
                {
                    Handles.DrawDottedLine(script.nodes[script.nodes.Length - 1], script.nodes[0], 20f);
                }
            }
        }
        else
        {
            if (script.drawLastToFirst == true && script.nodes.Length != 0)
            {
                for (int i = 0; i < script.nodes.Length - 1; i++)
                {
                    Handles.DrawLine(script.nodes[script.nodes.Length - 1], script.nodes[0]);
                }
            }
        }
        #endregion

        #region Undo
        Undo.RecordObject(target, "Node Follow Changes");
        #endregion
    }
}