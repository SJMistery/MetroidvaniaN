using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Taunt : StateMachineBehaviour
{
    Collider2D collider;
    Rigidbody2D rb;
    private BossController2D bossController;
    public int nOfMace = 5; //numero de mazas que se generan cuando el boss entra en modo taunt.
    private float waitTimer = 2.5f; //Timer que controla el tiempo antes de pasar al siguiente estado.

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider = animator.GetComponent<BoxCollider2D>();
        rb = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<BossController2D>();
        bossController.maceIsSpawned = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;    //Congela todas las direcciones para que al morir, para que el PJ no salga disparado

        if (!bossController.maceIsSpawned)
        {
            for (int i = 0; i <= nOfMace; i++)
            {
                bossController.SpawNewMace();
            }
            bossController.maceIsSpawned = true;
        }
        bossController.possibleSpaw.Clear();
        bossController.StartCoroutine(bossController.waitSeconds(waitTimer));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
