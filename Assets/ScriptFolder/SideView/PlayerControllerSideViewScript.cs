using UnityEngine;

public class PlayerControllerSideViewScript : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float sprintSpeed = 1f;
    public float jumpForce = 5f;
    private int doubleJump = 2;
    private Rigidbody2D rb;
    public Animator animator;
    public MagicAttackSpawner magicAttackSpawner;
    // SpriteRenderer spriteRenderer;
    public bool isSliding = false;
    
    bool isWalledLeft = false;
    bool isWalledRight = false;

    private float slideTimer = 0f;
    float slideDuration = 0.5f;
    string direction = "Right";

    public float health = 100f;

    public float knockBackForce;
    public float knockBackCounter;
    public float knockBackTotalTime;
    public bool knockFromRight;


    bool isBlocking;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        float moveInput = 0f;
        bool isMoving = false;
        if (direction == "Right" && transform.localScale.x == -1)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        if (direction == "Left" && transform.localScale.x == 1)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }

        if (!isBlocking)
        {
            if (Input.GetKeyDown(KeyCode.W) && doubleJump > 1)
            {
                animator.SetTrigger("Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // jump
                doubleJump -= 1;

            }
            if (IsGroundedScript.Instance.getGrounded())
            {
                doubleJump = 2;
            }



            if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
            {
                slideTimer = slideDuration;
                isSliding = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && isSliding)
            {
                isSliding = false;
            }


            if (Input.GetKey(KeyCode.A) && !isWalledLeft)
            {
                isMoving = true;
                direction = "Left";
                moveInput -= 1;
                if (hitAreaScript.Instance.getCircleOffset().x > 0) hitAreaScript.Instance.flipCircleOffset();
            }
            if (Input.GetKey(KeyCode.D) && !isWalledRight)
            {
                isMoving = true;
                direction = "Right";
                moveInput += 1;
                if (hitAreaScript.Instance.getCircleOffset().x < 0) hitAreaScript.Instance.flipCircleOffset();
                // animator.Play("RunRight");
            }
        }
        
        float moveSpeedTemp = moveSpeed;

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            moveSpeedTemp += sprintSpeed;
            if (slideTimer <= 0)
            {
                isSliding = false;
            }
        }

        animator.SetBool("isGrounded", IsGroundedScript.Instance.getGrounded());
        animator.SetBool("isFacingRight", direction == "Right");
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isSliding", isSliding);
        animator.SetFloat("xVelocity", rb.linearVelocityX);
        animator.SetFloat("yVelocity", rb.linearVelocityY);

        if (Input.GetKeyDown(KeyCode.L) && !isBlocking) {animator.Play("Block"); animator.SetBool("isBlocking", true);isBlocking = true; }
        if (Input.GetKeyUp(KeyCode.L) && isBlocking) {animator.SetBool("isBlocking",false);isBlocking = false; }
        
        
        if (knockBackCounter <= 0)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeedTemp, rb.linearVelocity.y);

        }
        else
        {
            if (knockFromRight)
            {
                rb.linearVelocity = new Vector2(-knockBackForce, knockBackForce);
            }
            if (!knockFromRight)
            {
                rb.linearVelocity = new Vector2(knockBackForce, knockBackForce);
            }

            knockBackCounter -= Time.deltaTime;
        }
    }

    public void attack(float damage)
    {
        if (!isBlocking)
        {
            knockBackCounter = knockBackTotalTime;
            health -= damage;
        }
        // if (isBlocking) {animator.Play("Parry"); isBlocking = false; }
        if (isBlocking) {animator.SetTrigger("Attacked"); isBlocking = false; }
        
        Debug.Log(health);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 contactPoint = contact.point;
                Vector2 center = transform.position;

                float direction = contactPoint.x - center.x;

                if (direction < 0)
                {
                    Debug.Log("Hit from the LEFT");
                    isWalledLeft = true;
                }
                else if (direction > 0)
                {
                    Debug.Log("Hit from the RIGHT");
                    isWalledRight = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalledLeft = false;
            isWalledRight = false;
        }
    }
}
