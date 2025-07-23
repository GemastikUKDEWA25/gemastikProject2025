using UnityEngine;

public class LineTesting : MonoBehaviour
{
    [SerializeField] public Transform pointStart;
    [SerializeField] public Transform pointEnd;
    [SerializeField] public LineScript lineController;
    [SerializeField] public PatrolEnemyScript enemyScript;

    public AudioClip AlertSound;
    public AudioSource source;

    private bool playerDetected = false;
    private Transform playerTransform;

    public float moveSpeed = 10f;
    public float maxDistance = 8f;

    private Vector2 lastDirection = Vector2.right; // Default direction

    void Start()
    {
        Transform[] points = { pointStart, pointEnd };
        lineController.SetUpLine(points);
    }

    void Update()
    {
        Vector2 direction = enemyScript.direction.normalized;

        if (direction != Vector2.zero)
            lastDirection = direction;

        // If player is detected, follow them with raycast check
        if (playerDetected && playerTransform != null)
        {
            Vector2 toPlayer = (playerTransform.position - pointStart.position).normalized;
            float distanceToPlayer = Vector2.Distance(pointStart.position, playerTransform.position);

            RaycastHit2D followHit = Physics2D.Raycast(pointStart.position, toPlayer, distanceToPlayer, LayerMask.GetMask("Obstacles", "Player"));

            if (followHit.collider != null && !followHit.collider.CompareTag("Player"))
            {
                Debug.Log("Player is now hidden!");

                playerDetected = false;
                enemyScript.setPlayerSeen(false);
                playerTransform = null;

                // Reset to max distance in last known direction
                pointEnd.position = pointStart.position + (Vector3)(lastDirection * maxDistance);
            }
            else
            {
                pointEnd.position = playerTransform.position;
            }
        }
        else
        {
            // Gradually move pointEnd in current direction
            pointEnd.position += (Vector3)(lastDirection * moveSpeed * Time.deltaTime);

            // Clamp distance
            float currentDistance = Vector2.Distance(pointStart.position, pointEnd.position);
            if (currentDistance > maxDistance)
            {
                pointEnd.position = pointStart.position + (Vector3)(lastDirection * maxDistance);
            }

            // Raycast to detect player or obstacle
            Vector2 rayDir = (pointEnd.position - pointStart.position).normalized;
            float rayDist = Vector2.Distance(pointStart.position, pointEnd.position);
            RaycastHit2D hit = Physics2D.Raycast(pointStart.position, rayDir, rayDist, LayerMask.GetMask("Obstacles", "Player"));

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Hit object: " + hitObject.name);
                pointEnd.position = hit.point;

                if (hitObject.CompareTag("Player") && !playerDetected)
                {
                    source.PlayOneShot(AlertSound);
                    enemyScript.setPlayerSeen(true);
                    playerTransform = hitObject.transform;
                    playerDetected = true;
                }
            }
            else
            {
                // No hit, ensure end reaches full max range
                pointEnd.position = pointStart.position + (Vector3)(lastDirection * maxDistance);

                enemyScript.setPlayerSeen(false);
                playerDetected = false;
                playerTransform = null;
            }
        }

        // Update line
        Transform[] points = { pointStart, pointEnd };
        lineController.SetUpLine(points);
    }

    // public void setPlayerDetected(bool status)
    // {
    //     playerDetected = status;
    // }
}
