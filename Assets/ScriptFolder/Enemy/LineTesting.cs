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

    public float timerUnsee = 5f;
    GameObject[] PatrolEnemy;
    void Start()
    {
        PatrolEnemy = GameObject.FindGameObjectsWithTag("PatrolEnemy");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Debug.Log(timerUnsee);
        Vector2 direction = enemyScript.direction.normalized;

        if (direction != Vector2.zero) lastDirection = direction;

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

        if (currentDistance > maxDistance)
        {
            pointEnd.position = pointStart.position + (Vector3)(lastDirection * maxDistance);
        }
        else
        {
            // === Overlap check for player in any direction ===
            Collider2D playerInRange = Physics2D.OverlapCircle(
                pointStart.position,
                maxDistance-1.5f,
                LayerMask.GetMask("Player")
            );

            if (playerInRange != null && playerInRange.CompareTag("Player"))
            {
                timerUnsee = 5f;
                for (int i = 0; i < PatrolEnemy.Length; i++)
                {
                    LineTesting otherEnemyScript = PatrolEnemy[i].GetComponent<LineTesting>();
                    otherEnemyScript.timerUnsee = 5f;
                }

                if (!playerDetected)
                {
                    source.PlayOneShot(AlertSound);
                    enemyScript.setPlayerSeen(true);
                    playerTransform = playerInRange.transform;
                    playerDetected = true;

                    for (int i = 0; i < PatrolEnemy.Length; i++)
                    {
                        LineTesting otherEnemyScript = PatrolEnemy[i].GetComponent<LineTesting>();
                        otherEnemyScript.setAlert();
                    }
                }

                // Optional: snap end point to where player was seen
                pointEnd.position = playerInRange.transform.position;
            }
            else
            {
                if (playerDetected)
                    timerUnsee -= Time.deltaTime;
            }

            // === Update line visual ===
            Transform[] points = { pointStart, pointEnd };
            lineController.SetUpLine(points);
        }

        if (timerUnsee <= 0)
        {
            enemyScript.setPlayerSeen(false);
            playerDetected = false;
            timerUnsee = 5f;
        }
    }

    public void TriggerChase()
    {
        pointEnd.position = playerTransform.position;
        if(!playerDetected)source.PlayOneShot(AlertSound);  
        setAlert();
        for (int i = 0; i < PatrolEnemy.Length; i++)
        {
            LineTesting otherEnemyScript = PatrolEnemy[i].GetComponent<LineTesting>();
            otherEnemyScript.setAlert();
        }
    }

    public void setAlert()
    {
        // source.PlayOneShot(AlertSound);  

        enemyScript.setPlayerSeen(true);
        playerTransform = playerTransform.transform;
        playerDetected = true;
    }

    void OnDrawGizmosSelected()
    {
        if (pointStart != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointStart.position, maxDistance);
        }
    }
}
