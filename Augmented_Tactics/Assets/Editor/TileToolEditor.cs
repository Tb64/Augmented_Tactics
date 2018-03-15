using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileTool))]
[CanEditMultipleObjects]
public class TileToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;

        TileTool myScript = (TileTool)target;
        if (GUILayout.Button("Set selected to Walkable"))
        {
            myScript.SelectedWalkable(Selection.gameObjects);
        }
        if (GUILayout.Button("Set selected to Un-walkable"))
        {
            myScript.SelectedUnwalkable(Selection.gameObjects);
        }
    }
}
