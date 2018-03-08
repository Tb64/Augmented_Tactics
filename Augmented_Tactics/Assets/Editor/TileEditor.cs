using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilePosGenerator))]
public class TileEditor : Editor {


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilePosGenerator myScript = (TilePosGenerator)target;
        if (GUILayout.Button("Show Tiles in Editor"))
        {
            myScript.ShowTiles();
        }
        if (GUILayout.Button("Hide Tiles in Editor"))
        {
            myScript.HideTiles();
        }
    }
}
