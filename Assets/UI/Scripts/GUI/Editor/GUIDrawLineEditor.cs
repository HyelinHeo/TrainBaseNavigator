using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GUIDrawLine))]
public class GUIDrawLineEditor : Editor
{
    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUIDrawLine gUIArrow = (GUIDrawLine)target;
        if (GUILayout.Button("Draw Line"))
        {
            gUIArrow.DrawLine();
        }
        if (GUILayout.Button("Show"))
        {
            gUIArrow.Show();
        }
        if (GUILayout.Button("Hide"))
        {
            gUIArrow.Hide();
        }
        if (GUILayout.Button("Init"))
        {
            gUIArrow.Init();
        }
    }
}
