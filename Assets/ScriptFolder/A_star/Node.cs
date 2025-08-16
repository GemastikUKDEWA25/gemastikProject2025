using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    
    // Always read live position so it's correct in Play AND Editor mode
    public Vector2 position => transform.position;

    public List<Node> connections = new List<Node>();

    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        if (connections != null && connections.Count > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i] != null)
                {
                    Gizmos.DrawLine(transform.position, connections[i].transform.position);
                }
            }
        }
    }
}
