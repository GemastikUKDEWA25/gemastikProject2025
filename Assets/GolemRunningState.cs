using UnityEngine;

public class GolemRunningState : StateMachineBehaviour
{
    GolemScript golem;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        golem = animator.GetComponent<GolemScript>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // golem.isAttacking = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        golem.FollowPlayer();
        golem.FlipTowardsPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
