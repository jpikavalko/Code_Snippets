using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MidpointDisplacement))]
public class MDEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MidpointDisplacement midpointPlacementScript = (MidpointDisplacement)target;

        if (GUILayout.Button("Generate new line"))
        {
            midpointPlacementScript.Generate(1);
        }
    }
}
