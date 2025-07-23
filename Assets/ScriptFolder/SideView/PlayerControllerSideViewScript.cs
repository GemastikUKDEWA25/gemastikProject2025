using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControllerSideViewScript : MonoBehaviour
{
    float moveSpeed = 10f;
    float sprintSpeed = 5f;
    float jumpForce = 15f;
    private int doubleJump = 2;
    private Rigidbody2D rb;
    bool isSliding = false;
    private float slideTimer = 0f;
    float slideDuration = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && doubleJump > 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
    }

    void FixedUpdate()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput -= 1;
            if (hitAreaScript.Instance.getCircleOffset().x > 0)
            {
                hitAreaScript.Instance.flipCircleOffset();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveInput += 1;
            
            if (hitAreaScript.Instance.getCircleOffset().x < 0)
            {
                hitAreaScript.Instance.flipCircleOffset();
            }
        }

        float moveSpeedTemp = moveSpeed;

        if (isSliding)
        {
            moveSpeedTemp += sprintSpeed;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeedTemp, rb.linearVelocity.y);
    }

}
