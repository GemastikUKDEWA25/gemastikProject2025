using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TrashMonsterScript : MonoBehaviour
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
    public float speed = 3f;

    private Vector3 lastPosition;
    public Vector2 direction;

    private Animator animator; // Animator reference



    public AudioClip[] idleSounds;          
    [Range(0f, 1f)] public float idleChancePerSec = 0.1f; 
    private float idleTimer = 1f;
    private AudioSource audioSource;

    public float idleStartDelay = 40f; // tunggu sebelum idle sound aktif
    private float idleStartTimer;
    public NodeGenerator mainRoute;
    private void Start()
    {
        curHealth = maxHealth;
        player = GameObject.Find("Player").GetComponent<PlayerControllerScript>();
        lastPosition = transform.position;
        animator = GetComponent<Animator>(); // Get Animator from the same GameObject

        // --- Tambahan: setup AudioSource ---
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();


        idleStartTimer = idleStartDelay;
        
        // currentNode = AStarManager.instance.FindNearestNode(transform.position, AStarManager.instance.AllNodes());
    }

    private void Update()
    {
        if (idleStartTimer > 0f)
        {
            idleStartTimer -= Time.deltaTime;
        }
        else if ((animator == null || !animator.GetBool("isPickedUp")) && !audioSource.isPlaying)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                idleTimer = 1f;
                if (idleSounds != null && idleSounds.Length > 0 && Random.value < idleChancePerSec)
                {
                    audioSource.PlayOneShot(idleSounds[Random.Range(0, idleSounds.Length)]);
                }
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

        // Call behavior functions
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

        // Move along the path
        CreatePath();

        // Calculate movement delta AFTER moving
        Vector3 moveDelta = transform.position - lastPosition;
        direction = new Vector2(moveDelta.x, moveDelta.y).normalized;

        // Animation control: Moving if actually traveling toward a target
        bool isMoving = moveDelta.sqrMagnitude > 0.0001f;
        if (!animator.GetBool("isPickedUp")) animator.SetBool("isMoving", isMoving);

        // Store position for next frame
        lastPosition = transform.position;
    }

    void Patrol()
    {
        // alertMark.SetActive(false);

        currentNode = mainRoute.AssignEnemyNodes(transform);
        Debug.Log($"Path is null {path == null}");
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(
                currentNode,
                // AStarManager.instance.AllNodes()[Random.Range(0, AStarManager.instance.AllNodes().Length)]
                mainRoute.getAllNodes()[Random.Range(0, mainRoute.getAllNodes().Length - 1)]
            );
        }
    }
    void Engage()
    {
        // alertMark.SetActive(true);

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
}
