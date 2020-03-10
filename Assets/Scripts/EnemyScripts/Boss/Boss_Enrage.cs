using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enrage : StateMachineBehaviour
{

    public int nOfRepetitions;
    BossController2D bossController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.GetComponent<BossController2D>();
        for (int i = 0; i <= nOfRepetitions; i++)
        {
            animator.Play("TauntEnrage");
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.ActivateCollidersAgain();
    }
}
