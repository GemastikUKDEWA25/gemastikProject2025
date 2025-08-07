
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    public float jumpForce = 10f;
    float movementSpeed = 3f;
    public float delay;
    float timer;
    public AudioClip stomp;
    GolemScript golem;
    Rigidbody2D rb;

    string direction = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        golem = animator.GetComponent<GolemScript>();
        timer = delay;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (golem.isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // jump
            golem.playSound(stomp);
            golem.isGrounded = false;
        }

        // if (direction == null) direction = golem.getFacingDirection();
        direction = golem.getFacingDirection();
        float moveInput = 0f;
        if (direction == "Right")
        {
            // golem.FlipTowardsPlayer();
            moveInput += 1f;
        }
        if (direction == "Left")
        {
            // golem.FlipTowardsPlayer();
            moveInput -= 1f;
        }

        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);
        timer -= Time.deltaTime;
        animator.SetFloat("WheelRollTimer", timer);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = delay;
        golem.FlipTowardsPlayer();
        direction = null;
        golem.chance = -1;
    }

}
