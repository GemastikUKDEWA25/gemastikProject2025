using Unity.VisualScripting;
using UnityEngine;

public class LineTesting : MonoBehaviour
{
    [SerializeField] public Transform pointStart;
    [SerializeField] public Transform pointEnd;
    [SerializeField] public LineScript lineController;
    [SerializeField] public PatrolEnemyScript enemyScript;

    public AudioClip AlertSound;
    public AudioSource source;

    public bool playerDetected = false;
    private Transform playerTransform;

    public float moveSpeed = 10f;
    public float maxDistance = 8f;

    private Vector2 lastDirection = Vector2.right; // Default direction

    public bool chasePlayer;
    public bool isAlreadyAlert = false;

    public bool isPlayerHiding;

    float timerUnsee = 5f;

    GameObject[] PatrolEnemy;

    void Start()
    {
        PatrolEnemy = GameObject.FindGameObjectsWithTag("PatrolEnemy");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
            // Debug.Log(playerTransform.position);
        Debug.Log(timerUnsee);
        Vector2 direction = enemyScript.direction.normalized;

        if (direction != Vector2.zero)
            lastDirection = direction;

        // === Smoothly follow player if detected ===
        if (playerDetected && playerTransform != null)
        {
            pointEnd.position = Vector2.MoveTowards(
                pointEnd.position,
                playerTransform.position,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            // === Smoothly move in scanning direction ===
            Vector2 targetPoint = (Vector2)pointStart.position + (lastDirection * maxDistance);
            pointEnd.position = Vector2.MoveTowards(
                pointEnd.position,
                targetPoint,
                moveSpeed * Time.deltaTime
            );
        }

        // Clamp distance (still needed for edge cases)
        float currentDistance = Vector2.Distance(pointStart.position, pointEnd.position);
        // == Raycast to detect player or obstacle ===
        Vector2 rayDir = (pointEnd.position - pointStart.position).normalized;
        float rayDist = Vector2.Distance(pointStart.position, pointEnd.position);
        RaycastHit2D hit = Physics2D.Raycast(pointStart.position, rayDir, rayDist, LayerMask.GetMask("Obstacles", "Player"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            GameObject hitObject = hit.collider.gameObject;
            pointEnd.position = hit.point;
            if (hitObject.CompareTag("Player") && hitObject.layer == LayerMask.NameToLayer("Player"))
            {
                timerUnsee = 5f;
                TriggerChase();
            }
        }
        
        if(hit.collider == null || isPlayerHiding)
        {
            // No hit, ensure end reaches full max range
            // pointEnd.position = pointStart.position + (Vector3)(lastDirection * maxDistance);
            if (playerDetected) timerUnsee -= Time.deltaTime; // if enemy see and still chased timer going to 
        }

        if (timerUnsee <= 0)
        {
            setToPatrol();
            timerUnsee = 5f;
        }
        
        // === Update line visual ===
        Transform[] points = { pointStart, pointEnd };
        lineController.SetUpLine(points);
    }

    public void TriggerChase()
    {
        for (int i = 0; i < PatrolEnemy.Length; i++)
        {
            LineTesting otherEnemyScript = PatrolEnemy[i].GetComponent<LineTesting>();
            otherEnemyScript.setAlert();
        }
        pointEnd.position = playerTransform.position;
        if (!playerDetected) source.PlayOneShot(AlertSound);
        setAlert();
    }

    public void setAlert()
    {
        // source.PlayOneShot(AlertSound);  

        enemyScript.setPlayerSeen(true);
        playerTransform = playerTransform.transform;
        playerDetected = true;
    }

    public void setToPatrol()
    {
        enemyScript.setPlayerSeen(false);
        // playerTransform = playerTransform.transform;
        playerDetected = false;
    }

    public void setIsPlayerHiding(bool isHiding)
    {
        for (int i = 0; i < PatrolEnemy.Length; i++)
        {
            LineTesting otherEnemyScript = PatrolEnemy[i].GetComponent<LineTesting>();
            otherEnemyScript.isPlayerHiding = isHiding;
        }
    }
}
