#if UNITY_EDITOR
using System.Collections;

using UnityEditor;

using UnityEngine;

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
#endif