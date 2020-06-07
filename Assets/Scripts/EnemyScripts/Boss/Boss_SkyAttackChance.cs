using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SkyAttackChance : StateMachineBehaviour
{
    int num1 = 0, num2 = 0;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        num1 = Random.Range(1, 3);
        num2 = Random.Range(1, 3);
        if (num1 == num2)
        {
            animator.SetBool("SkyAttack", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
