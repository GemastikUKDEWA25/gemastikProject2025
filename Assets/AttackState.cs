
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    Rigidbody2D rb;
    float movementSpeed = 3f;
    float timer = 5f;
    GolemScript golem;

    string direction = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        golem = animator.GetComponent<GolemScript>();
        timer = 5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("WheelRollTimer",timer);
        if (direction == null) direction = golem.getFacingDirection();

        float moveInput = 0f;
        if (direction == "Right")
        {
            if (golem.getScale().x > 0) golem.Flip();
            moveInput += 1f;
        }
        if (direction == "Left")
        {
            if (golem.getScale().x < 0) golem.Flip();
            moveInput -= 1f;

        }

        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            golem.FlipTowardsPlayer();
            direction = null;
            timer = 5f;
            golem.timer = 3f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 5f;
        golem.FlipTowardsPlayer();
        direction = null;
        golem.chance = -1;
    }

}
