using UnityEngine;
using TMPro;

public class GolemScript : MonoBehaviour
{
    float health = 100;
    public static EnemyScript Instance { get; private set; }

    public float eyeMovementRadius = 0.3f;
    Transform player;
    private Vector3 centerPosition;
    public GameObject golemEye;
    public TextMeshProUGUI healthText;
    Animator animator;
    bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        centerPosition = golemEye.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player == null);
        Vector3 localPlayerPos = golemEye.transform.parent.InverseTransformPoint(player.position);
        Vector3 directionToPlayer = (localPlayerPos - centerPosition).normalized;
        
        // Calculate how far the eye should move
        float distanceToMove = Mathf.Min(eyeMovementRadius, Vector3.Distance(centerPosition, localPlayerPos));
        
        // Set the eye position
        golemEye.transform.localPosition = centerPosition + directionToPlayer * distanceToMove;


        if (health <= 0)
        {
            if (!isDead)
            {
                animator.SetTrigger("Dead");
                isDead = true;
            }
            // Destroy(gameObject);
        }
        healthText.text = health.ToString();

    }

    public void attack(float damage)
    {
        health -= damage;
    }
    public float getHealth()
    {
        return health;
    }

    public void chasePlayer(Rigidbody2D rb, PlayerControllerSideViewScript player, float movementSpeed)
    {
        float direction = player.transform.position.x - transform.position.x;

        // Only move if far enough (optional threshold)
        if (Mathf.Abs(direction) > 0.01f)
        {
            float move = Mathf.Sign(direction) * movementSpeed * Time.fixedDeltaTime;
            Vector2 newPos = new Vector2(transform.position.x + move, transform.position.y);
            rb.MovePosition(newPos);

            // Optional: Flip to face direction
            transform.localScale = new Vector3(Mathf.Sign(direction), 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "hitArea")
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                attack(1);
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "hitArea")
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                attack(1);       
            }
        }
    }
}
