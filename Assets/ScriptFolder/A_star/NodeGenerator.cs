using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class NodeGenerator : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject floor;
    public float spacing = 1f;

    public LayerMask obstacleMask; // Assign "Obstacles" layer in inspector

    // public static List<Node> allNodes = new List<Node>();
    public List<Node> allNodes = new List<Node>();
    

    void Start()
    {
        if (allNodes.Count <= 0)
        {
            GenerateAndConnectAndAssign();
        }
        // allNodes.Clear();
        // foreach (Transform child in transform)
        // {
        //     allNodes.Add(child.gameObject.GetComponent<Node>());
        // }
    }

    // === MAIN SEQUENCE (same for button and Start) ===
    public void GenerateAndConnectAndAssign()
    {
        ClearNodes();
        GenerateNodes();
        ConnectNodes();
        // AssignEnemyStartNodes();
    }

    public void GenerateNodes()
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

    // public void AssignEnemyStartNodes()
    // {
    //     // Patrol enemies
    //     PatrolEnemyScript[] enemies = FindObjectsByType<PatrolEnemyScript>(FindObjectsSortMode.None);
    //     foreach (var enemy in enemies)
    //     {
    //         Node closest = allNodes
    //             .OrderBy(n => Vector2.Distance(n.position, enemy.transform.position))
    //             .FirstOrDefault();
    //         if (closest != null)
    //             enemy.currentNode = closest;
    //     }

    //     // Trash monsters
    //     TrashMonsterScript[] trashMonsters = FindObjectsByType<TrashMonsterScript>(FindObjectsSortMode.None);
    //     foreach (var monster in trashMonsters)
    //     {
    //         Node closest = allNodes
    //             .OrderBy(n => Vector2.Distance(n.position, monster.transform.position))
    //             .FirstOrDefault();
    //         if (closest != null)
    //             monster.currentNode = closest;
    //     }
    // }

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
        // if (closest != null)
        //     enemy.currentNode = closest;
        return closest;

    }
}
