using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GUIPlayTime))]
public class GUIPlayPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUIPlayTime gUIPlayTime = (GUIPlayTime)target;
        if (GUILayout.Button("Draw Line"))
        {
            gUIPlayTime.SetOffset();
        }
    }
}
