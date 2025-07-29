using UnityEngine;

public class Randomizer : StateMachineBehaviour
{
    public int minRandom;
    public int maxRandom;
    public float delay;
    float timer;
    public string paramName;
    GolemScript golem;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        golem = animator.GetComponent<GolemScript>();
        timer = delay;
        animator.SetInteger(paramName, -1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            int randint = Random.Range(minRandom, maxRandom);
            animator.SetInteger(paramName, randint);
            timer = delay;
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = delay;
        animator.SetInteger(paramName, -1);
        golem.isAttacking = true;
    }
}
