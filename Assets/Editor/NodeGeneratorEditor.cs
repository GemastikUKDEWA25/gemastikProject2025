using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodeGenerator))]
public class NodeGeneratorEditor : Editor
{
    SerializedProperty generationModeProp;
    SerializedProperty floorProp;
    SerializedProperty edgeColliderProp;
    SerializedProperty spacingProp;
    SerializedProperty circling;

    private void OnEnable()
    {
        generationModeProp = serializedObject.FindProperty("generationMode");
        floorProp = serializedObject.FindProperty("floor");
        edgeColliderProp = serializedObject.FindProperty("edgeCollider");
        spacingProp = serializedObject.FindProperty("spacing");
        circling = serializedObject.FindProperty("circling");   // ✅ FIXED
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Dropdown for generation mode
        EditorGUILayout.PropertyField(generationModeProp);

        // Show fields based on dropdown selection
        NodeGenerator.GenerationMode mode = 
            (NodeGenerator.GenerationMode)generationModeProp.enumValueIndex;

        switch (mode)
        {
            case NodeGenerator.GenerationMode.FloorGrid:
                EditorGUILayout.PropertyField(floorProp);
                EditorGUILayout.PropertyField(spacingProp);
                break;

            case NodeGenerator.GenerationMode.EdgeCollider:
                EditorGUILayout.PropertyField(edgeColliderProp);
                EditorGUILayout.PropertyField(spacingProp);
                EditorGUILayout.PropertyField(circling);  // ✅ Now works
                break;
        }

        EditorGUILayout.Space();

        // Buttons
        if (GUILayout.Button("Generate & Connect & Assign"))
        {
            ((NodeGenerator)target).GenerateAndConnectAndAssign();
        }

        if (GUILayout.Button("Clear Nodes"))
        {
            ((NodeGenerator)target).ClearNodes();
        }

        serializedObject.ApplyModifiedProperties(); // ✅ saves inspector edits
    }
}
