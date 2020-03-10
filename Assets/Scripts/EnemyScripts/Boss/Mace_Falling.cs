using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace_Falling : StateMachineBehaviour
{
    Rigidbody2D rb;
    Collider2D collider;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        collider = animator.GetComponent<CircleCollider2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // cambia el rigidbody a dinamico para que el objeto empiece a caer.
        rb.bodyType = RigidbodyType2D.Dynamic; 
        // Se activa el Collider para que se le aplique el daño al Pj.
        collider.enabled = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
