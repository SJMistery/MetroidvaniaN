using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    public float speed = 5f;
    public float attRange = 1f;
    private float currentCD = 0;
    public float attackCD = 10f;

    Transform player;
    Rigidbody2D rb;
    BossController2D cntlrBoss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("SrBeta1").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        cntlrBoss = animator.GetComponent<BossController2D>();
        currentCD = attackCD;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cntlrBoss.LookAtPlayer();
        currentCD -= Time.deltaTime;

        Vector2 target = new Vector2(player.position.x, rb.position.y);                             //creates a new vector with the position of the player
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime); //utiliza esa variable para hacer que el enemigo se mueva direccion hacia el jugador.
        rb.MovePosition(newPosition);

        if (Vector2.Distance(player.position, rb.position) <= attRange && currentCD <= 0)
        {
            //Make the boss attack.
            animator.SetTrigger("Attack");
            currentCD = attackCD;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("SkyAttack");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
