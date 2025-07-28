using UnityEngine;

public class PlayerControllerSideViewScript : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float sprintSpeed = 1f;
    public float jumpForce = 5f;
    private int doubleJump = 2;
    private Rigidbody2D rb;
    private Animator animator;
    bool isSliding = false;
    private float slideTimer = 0f;
    float slideDuration = 0.5f;
    string direction = "Right";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = 0f;
        bool isMoving = false;
        
        if (Input.GetKeyDown(KeyCode.W) && doubleJump > 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // jump
            doubleJump -= 1;
        }
        if (IsGroundedScript.Instance.getGrounded())
        {
            doubleJump = 2;
        }
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                isSliding = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            isSliding = true;
            slideTimer = slideDuration;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            direction = "Left";
            moveInput -= 1;
            if (hitAreaScript.Instance.getCircleOffset().x > 0) hitAreaScript.Instance.flipCircleOffset();
            animator.Play("RunLeft");
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            direction = "Right";
            moveInput += 1;
            if (hitAreaScript.Instance.getCircleOffset().x < 0) hitAreaScript.Instance.flipCircleOffset();
            animator.Play("RunRight");
        }
        if (!isMoving)
        {
            if (direction == "Left") animator.Play("IdleLeft");
            if (direction == "Right") animator.Play("IdleRight");
        }

        float moveSpeedTemp = moveSpeed;

        if (isSliding)
        {
            moveSpeedTemp += sprintSpeed;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeedTemp, rb.linearVelocity.y);
    }

    void FixedUpdate()
    {
        
    }

}
