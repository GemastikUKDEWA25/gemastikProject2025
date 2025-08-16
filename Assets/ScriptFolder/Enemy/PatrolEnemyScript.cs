using System.Collections.Generic;
using UnityEngine;
public class PatrolEnemyScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public int panicMultiplier = 1;
    private bool playerSeen;
    public GameObject alertMark;
    public Node currentNode;
    public List<Node> path = new List<Node>();
    
    
    public enum StateMachine
    {
        Patrol,
        Engage,
        Evade
    }
    public StateMachine currentState;
    private PlayerControllerScript player;
    public float speed = 25f;
    private Vector3 lastPosition;
    public Vector2 direction;  // this stores the movement direction

    public BulletSpawner bulletSpawner;
    float bulletTimer = 0.2f;
    float bulletDelay = 0.2f;

    [Header("Route")]
    public NodeGenerator patrolRoute;
    public NodeGenerator mainRoute;


    private void Start()
    {
        curHealth = maxHealth;
        player = GameObject.Find("Player").GetComponent<PlayerControllerScript>();
        lastPosition = transform.position;
        alertMark.SetActive(false);
    }
    private void Update()
    {
        // Debug.Log("State: " + currentState + " | Seen: " + playerSeen + " | HP: " + curHealth);

        // State transitions

        if (currentState == StateMachine.Engage)
        {
            if (bulletTimer <= 0)
            {
                bulletSpawner.Spawn();
                bulletTimer = bulletDelay;
            }
            else
            {
                bulletTimer -= Time.deltaTime;
            }
        }

        if (!playerSeen && currentState != StateMachine.Patrol && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Patrol;
            path.Clear();
        }
        else if (playerSeen && currentState != StateMachine.Engage && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Engage;
            path.Clear();
        }
        else if (currentState != StateMachine.Evade && curHealth <= (maxHealth * 20) / 100)
        {
            panicMultiplier = 2;
            currentState = StateMachine.Evade;
            path.Clear();
        }
        // Update path if needed
        switch (currentState)
        {
            case StateMachine.Patrol:
                Patrol();
                break;
            case StateMachine.Engage:
                Engage();
                break;
            case StateMachine.Evade:
                Evade();
                break;
        }
        // Move and update direction
        CreatePath();
        // Calculate direction (difference between this and last frame)
        Vector3 moveDelta = transform.position - lastPosition;
        direction = new Vector2(moveDelta.x, moveDelta.y).normalized;
        lastPosition = transform.position;
    }
    void Patrol()
    {
        alertMark.SetActive(false);

        currentNode = patrolRoute.AssignEnemyNodes(transform);
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(
                currentNode,
                // AStarManager.instance.AllNodes()[Random.Range(0, AStarManager.instance.AllNodes().Length)]
                patrolRoute.getAllNodes()[Random.Range(0, patrolRoute.getAllNodes().Length-1)]
            );
        }
    }
    void Engage()
    {
        alertMark.SetActive(true);

        currentNode = mainRoute.AssignEnemyNodes(transform);
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(
                currentNode,
                // AStarManager.instance.FindNearestNode(player.transform.position)
                AStarManager.instance.FindNearestNode(player.transform.position,mainRoute.getAllNodes())

            );
        }
    }
    void Evade()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(
                currentNode,
                // AStarManager.instance.FindFurthestNode(player.transform.position)
                AStarManager.instance.FindFurthestNode(player.transform.position,mainRoute.getAllNodes())
            );
        }
    }
    public void setToEngaged()
    {
        currentState = StateMachine.Patrol;
    }

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            int x = 0;
            Vector3 target = new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2);
            transform.position = Vector3.MoveTowards(transform.position, target, (speed * panicMultiplier) * Time.deltaTime);
            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }
    public void setPlayerSeen(bool isSeen)
    {
        playerSeen = isSeen;
    }
}