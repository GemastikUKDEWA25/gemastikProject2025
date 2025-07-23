using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public Vector2 position;
    public List<Node> connections;

    public float gScore;
    public float hScore;
    private void Awake()
    {
        position = transform.position;
    }
    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        if(connections.Count > 0)
        {
            Gizmos.color = Color.blue;
            for(int i = 0; i < connections.Count; i++)
            {
                if (connections[i] != null){
                    Gizmos.DrawLine(transform.position, connections[i].transform.position);
                }
            }
        }
    }
}