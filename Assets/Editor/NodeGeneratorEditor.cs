using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodeGenerator))]
public class NodeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Keep default inspector

        NodeGenerator nodeGenerator = (NodeGenerator)target;

        // Generate + Connect + Assign (same as Start)
        if (GUILayout.Button("Generate & Connect & Assign"))
        {
            nodeGenerator.GenerateAndConnectAndAssign();
        }

        if (GUILayout.Button("Generate Nodes Only"))
        {
            nodeGenerator.ClearNodes();
            nodeGenerator.GenerateNodes();
        }

        if (GUILayout.Button("Connect Nodes Only"))
        {
            nodeGenerator.ConnectNodes();
        }

        // if (GUILayout.Button("Assign Enemy Start Nodes"))
        // {
        //     nodeGenerator.AssignEnemyStartNodes();
        // }

        if (GUILayout.Button("Reset Connections"))
        {
            nodeGenerator.ResetConnections();
        }

        if (GUILayout.Button("Clear Nodes"))
        {
            nodeGenerator.ClearNodes();
        }
    }
}
