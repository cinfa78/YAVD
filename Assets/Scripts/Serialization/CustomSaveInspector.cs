#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveGameManager))]
public class CustomSaveInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveGameManager script = (SaveGameManager)target;

        if (GUILayout.Button("Save test file"))
        {
            script.SaveTestFile();
        }

        if (GUILayout.Button("Load test file"))
        {
            script.LoadTestFile();
        }
    }
}
#endif