using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class NodeGenerator : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject floor;
    public float spacing = 1f;

    public LayerMask obstacleMask; // Assign "Obstacles" layer in inspector

    public static List<Node> allNodes = new List<Node>();

    void Start()
    {
        GenerateNodes();
        ConnectNodes();
        AssignEnemyStartNodes();
    }

    void GenerateNodes()
    {
        allNodes.Clear();

        SpriteRenderer sr = floor.GetComponent<SpriteRenderer>();
        Vector2 startPos = sr.bounds.min;
        Vector2 size = sr.bounds.size;

        int cols = Mathf.FloorToInt(size.x / spacing);
        int rows = Mathf.FloorToInt(size.y / spacing);

        for (int x = 0; x <= cols; x++)
        {
            for (int y = 0; y <= rows; y++)
            {
                Vector2 spawnPos = startPos + new Vector2(x * spacing, y * spacing);

                // Check if spawnPos overlaps any collider in obstacleMask
                Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.1f, obstacleMask);
                if (hit == null)
                {
                    GameObject obj = Instantiate(nodePrefab, spawnPos, Quaternion.identity, transform);
                    Node node = obj.GetComponent<Node>();
                    allNodes.Add(node);
                }
            }
        }
    }

    void ConnectNodes()
    {
        foreach (Node node in allNodes)
        {
            node.connections = new List<Node>();

            foreach (Node other in allNodes)
            {
                if (other != node && Vector2.Distance(node.position, other.position) <= spacing * 1.5f)
                {
                    node.connections.Add(other);
                }
            }
        }
    }

    void AssignEnemyStartNodes()
    {
        PatrolEnemyScript[] enemies = FindObjectsByType<PatrolEnemyScript>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            Node closest = allNodes.OrderBy(n => Vector2.Distance(n.position, enemy.transform.position)).FirstOrDefault();
            if (closest != null)
            {
                enemy.currentNode = closest;
            }
        }
    }
}
