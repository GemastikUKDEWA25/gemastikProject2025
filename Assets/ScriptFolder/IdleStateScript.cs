using UnityEngine;

public class IdleStateScript : StateMachineBehaviour
{
    float timer = 0.1f;
    GolemScript golem;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        golem = animator.GetComponent<GolemScript>();
        golem.isAttacking = false;
        timer = 0.1f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        animator.SetFloat("Timer", timer);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.1f;
        animator.SetFloat("Timer", timer);
    }
}