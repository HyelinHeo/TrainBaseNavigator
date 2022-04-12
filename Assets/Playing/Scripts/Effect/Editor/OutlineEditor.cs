using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Outline))]
public class OutlineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Outline line = (Outline)target;
        //if (GUILayout.Button("Bake"))
        //{
        //    line.Bake();
        //}
        //if (GUILayout.Button("LoadSmoothNormals"))
        //{
        //    line.LoadSmoothNormals();
        //}
        if (GUILayout.Button("Refresh"))
        {
            line.Refresh();
        }
    }
}
