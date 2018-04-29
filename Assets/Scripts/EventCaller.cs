using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SEvent))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SEvent myScript = (SEvent)target;
        if (GUILayout.Button("Raise Event"))
        {
            myScript.Raise();
        }
    }
}