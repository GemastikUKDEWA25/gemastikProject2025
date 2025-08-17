using System.Linq;
using UnityEngine;
using System.Collections.Generic;


public class NodeGenerator : MonoBehaviour
{
    public enum GenerationMode
    {
        FloorGrid,
        EdgeCollider
    }

    public GenerationMode generationMode = GenerationMode.FloorGrid;
    public EdgeCollider2D edgeCollider;

    // variables specific to modes
    public GameObject nodePrefab;
    public GameObject floor;
    public float spacing = 1f;
    public bool circling = false;

    public LayerMask obstacleMask; // Assign "Obstacles" layer in inspector

    public List<Node> allNodes = new List<Node>();

    void Start()
    {
        if (allNodes.Count <= 0)
        {
            if (generationMode == GenerationMode.FloorGrid)
            {
                GenerateAndConnectAndAssign();
            }
            if (generationMode == GenerationMode.EdgeCollider)
            {
                GenerateNodesFromEdge(edgeCollider,spacing);
            }
        }
    }

    // === MAIN SEQUENCE (same for button and Start) ===
    public void GenerateAndConnectAndAssign()
    {
        ClearNodes();

        if (generationMode == GenerationMode.FloorGrid)
        {
            GenerateNodes();
        }
        else if (generationMode == GenerationMode.EdgeCollider)
        {
            GenerateNodesFromEdge(edgeCollider, spacing);
        }

        ConnectNodes();
    }

    public void GenerateNodes()
    {
        allNodes.Clear();

        if (floor == null)
        {
            Debug.LogError("Floor object not assigned!");
            return;
        }

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

    public void GenerateNodesFromEdge(EdgeCollider2D edge, float spacing = 1f)
    {
        if (edge == null)
        {
            Debug.LogError("EdgeCollider2D not assigned!");
            return;
        }

        allNodes.Clear();

        Vector2[] points = edge.points;

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 start = edge.transform.TransformPoint(points[i]);
            Vector2 end = edge.transform.TransformPoint(points[i + 1]);

            float dist = Vector2.Distance(start, end);
            int segments = Mathf.Max(1, Mathf.FloorToInt(dist / spacing));

            for (int j = 0; j <= segments; j++)
            {
                Vector2 pos = Vector2.Lerp(start, end, (float)j / segments);

                GameObject obj = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                Node node = obj.GetComponent<Node>();
                allNodes.Add(node);
            }
        }
        ConnectNodes();
    }

    public void ConnectNodes()
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

    public void ResetConnections()
    {
        foreach (Node node in allNodes)
        {
            node.connections.Clear();
        }
    }

    public void ClearNodes()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        allNodes.Clear();
    }

    public Node[] getAllNodes()
    {
        Debug.Log(allNodes.Count);
        return allNodes.ToArray();
    }

    public Node AssignEnemyNodes(Transform enemy)
    {
        Node closest = allNodes
            .OrderBy(n => Vector2.Distance(n.position, enemy.position))
            .FirstOrDefault();
        return closest;
    }
}
