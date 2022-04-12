using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(testShadow))]
public class testShadowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        testShadow test = (testShadow)target;
        if (GUILayout.Button("Shadow On"))
        {
            test.ShadowOn();
        }
        if (GUILayout.Button("Shadow Off"))
        {
            test.ShadowOff();
        }
        if (GUILayout.Button("Change Light Color"))
        {
            test.ChangeLightColor();
        }
        if (GUILayout.Button("Change Light Intensity"))
        {
            test.LightIntensity();
        }
        if (GUILayout.Button("New File"))
        {
            test.DestroyAllObjects();
        }
    }
}
