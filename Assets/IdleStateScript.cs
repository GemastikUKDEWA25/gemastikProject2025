using UnityEditor.Rendering;
using UnityEngine;

public class IdleStateScript : StateMachineBehaviour
{
    GolemScript golem;
    string[] arrOfAttack = {"Attack","RockPunch"};
    private bool hasTriggeredAttack = false; // Add this flag
    float timer = 3;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        golem = animator.GetComponent<GolemScript>();
        hasTriggeredAttack = false; // Reset flag when entering Idle state
        timer = 3;
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
        animator.SetFloat("Timer", timer);
        timer = 3;
    }
}