using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClickableTile))]
public class CTEditor : Editor
{


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;

        ClickableTile myScript = (ClickableTile)target;
        if (GUILayout.Button("Set Walkable"))
        {
            myScript.SetWalkable();
        }
        if (GUILayout.Button("Set Un-Walkable"))
        {
            myScript.SetUnwalkable();
        }
    }
}
