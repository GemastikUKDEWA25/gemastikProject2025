using UnityEngine;

using TMPro;

public class GolemScript : MonoBehaviour
{
    float health = 100;
    public static GolemScript Instance { get; private set; }

    public float eyeMovementRadius = 0.3f;
    Transform player;
    public Vector3 centerPosition;
    public GameObject golemEye;
    public TextMeshProUGUI healthText;
    Animator animator;
    public bool isDead = false;
    public bool isInAnimation = false;

    public float followSpeed = 2f;
    public float stopDistance = 1f;
    public bool facingRight = true;

    bool isRunning = false;
    bool isIdle = false;

    public bool isAttacking = false;
    float distanceToPlayer;

    public float timer = 3f;

    public int chance = -1;

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

        if (!isDead)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            FlipTowardsPlayer();
            
            animator.SetFloat("Distance", distanceToPlayer);
            animator.SetFloat("Health", health);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                chance = Random.Range(0, 5);
                timer = 3f;
            }
            animator.SetInteger("WheelRoll", chance);

            // -- golem eye --
            Vector3 localPlayerPos = golemEye.transform.parent.InverseTransformPoint(player.position);
            Vector3 directionToPlayer = (localPlayerPos - centerPosition).normalized;
            float distanceToMove = Mathf.Min(eyeMovementRadius, Vector3.Distance(centerPosition, localPlayerPos));
            golemEye.transform.localPosition = centerPosition + directionToPlayer * distanceToMove;
        }

        if (health <= 0)
        {
            if (!isDead)
            {
                animator.SetTrigger("Dead");
                isDead = true;
            }
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

    public string getFacingDirection()
    {   
        if (facingRight) return "Right";
        else return "Left";
    }

    public void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
    }
    
    public void FlipTowardsPlayer()
    {
        // Determine if player is to the right or left
        bool playerIsRight = player.position.x > transform.position.x;
        
        // Only flip if direction changed
        if (playerIsRight != facingRight)
        {
            facingRight = playerIsRight;
            Flip();
        }
    }
    
    public void Flip()
    {
        // Flip using x scale *= -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public Vector3 getScale()
    {
        Vector3 scale = transform.localScale;
        return scale;
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
