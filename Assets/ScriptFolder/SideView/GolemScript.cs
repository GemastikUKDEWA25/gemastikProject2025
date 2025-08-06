using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GolemScript : MonoBehaviour
{
    float Maxhealth = 500f;
    float health;
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
    public bool facingRight = true;

    public bool isAttacking = false;

    float distanceToPlayer;

    public float timer = 3f;

    public int chance = -1;

    public bool isGrounded = false;

    [Header("BodyParts")]
    public GolemHitAreaScript LeftHand;
    public GolemHitAreaScript RightHand;
    public GolemHitAreaScript LeftFoot;
    public GolemHitAreaScript RightFoot;
    public GolemHitAreaScript laser;
    

    [Header("Rocks")]
    public GolemHitAreaScript[] rocks;

    public Slider healthBar;

    public bool parryAvailable = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        centerPosition = golemEye.transform.localPosition;
        health = Maxhealth;
        healthBar.maxValue = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            int currentHealth = Mathf.Clamp(Mathf.FloorToInt(health), 0, Mathf.FloorToInt(Maxhealth));
            healthBar.value = currentHealth;
        }
        else { healthBar.fillRect.gameObject.SetActive(false); }

        if (!isDead)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            FlipTowardsPlayer();

            animator.SetFloat("Distance", distanceToPlayer);
            // animator.SetFloat("Health", health);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                chance = Random.Range(0, 4);
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

    }

    public void stunned()
    {
        animator.Play("GolemStunned");
        animator.SetBool("isStunned", true);
        parryAvailable = false;
    }

    public void setParryAvailableTrue()
    {
        parryAvailable = true;
    }
    public void setParryAvailableFalse()
    {
        parryAvailable = false;
    }
    

    public void attack(float damage)
    {
        if (health > 0) { health -= damage; }
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


    public void GolemPunch()
    {
        hitAreaNeutral();
        LeftHand.trigger = true;
        LeftFoot.trigger = true;
        RightHand.trigger = true;
        RightFoot.trigger = true;
        rocksHitAreaTriggerOn();
    }

    public void GolemRockPunch()
    {
        hitAreaNeutral();
        RightHand.trigger = true;
        LeftHand.trigger = true;
        rocksHitAreaTriggerOn();
    }

    public void GolemBasicAttack()
    {
        hitAreaNeutral();
        RightHand.trigger = true;
    }

    public void GolemRocketPunch()
    {
        hitAreaNeutral();
        RightHand.trigger = true;
    }

    public void GolemLaser()
    {
        hitAreaNeutral();
        laser.trigger = true;
    }

    public void GolemWheelRoll()
    {
        hitAreaNeutral();
        LeftHand.trigger = true;
        LeftFoot.trigger = true;
        RightHand.trigger = true;
        RightFoot.trigger = true;
    }

    void rocksHitAreaTriggerOn()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            rocks[i].trigger = true;
        }
    }

    void rocksHitAreaTriggerOff()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            rocks[i].trigger = false;
        }
    }



    public void hitAreaNeutral()
    {
        LeftHand.trigger = false;
        LeftFoot.trigger = false;
        RightHand.trigger = false;
        RightFoot.trigger = false;
        laser.trigger = false;
        rocksHitAreaTriggerOff();
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
