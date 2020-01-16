using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    [SerializeField] private Animator controller;
    [SerializeField] private int times = 0;
    [SerializeField] private bool animationState;
    [SerializeField] private bool inverted;
    [SerializeField] private bool afterjump;
    [SerializeField] private bool changeidle;
    [SerializeField] private bool change;
    [SerializeField] private bool standup;
    [SerializeField] private bool aux;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInChildren<Animator>();
        changeidle = true;
        standup = true;
        change = true;
        aux = true;
    }

    void RunningAttackAnimation()
    {
        controller.Play("attack3_animation");
        controller.SetBool("running", false);
        controller.SetBool("idle1", false);
    }

    void ChangeStanceto2()
    {
        controller.Play("Sword_Draw_animation");
        controller.SetBool("changeidle-2", true);
        controller.SetBool("idle2", true);
        controller.SetBool("idle1", false);
    }

    // Update is called once per frame
    void Update()
    {

        //controller.SetTrigger("running");

        animationState = controller.IsInTransition(0);
        if (Input.GetKey(KeyCode.D) && !controller.GetBool("crouch") && !controller.GetBool("jump"))
        {
            if (controller.GetBool("running"))
                controller.Play("run_animation");
            if (inverted)
                GetComponent<SpriteRenderer>().flipX = false;
            inverted = false;
            if (Input.GetKey(KeyCode.T))
                RunningAttackAnimation();
            else
                controller.SetBool("running", true);
        }

        if (Input.GetKey(KeyCode.A) && !controller.GetBool("crouch") && !controller.GetBool("jump"))
        {
            if (controller.GetBool("running"))
                controller.Play("run_animation");
            if (!inverted)
                GetComponent<SpriteRenderer>().flipX = true;
            inverted = true;
            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.D))
                RunningAttackAnimation();
            else
                controller.SetBool("running", true);
        }

        if (Input.GetKey(KeyCode.S) && !controller.GetBool("jump"))
        {
            controller.Play("crouch_animation");
            controller.SetBool("crouch", true);
        }
        else
            controller.SetBool("crouch", false);

        if (Input.GetKey(KeyCode.W) && !controller.GetBool("jump"))
        {
            controller.SetBool("jump", true);
            controller.SetBool("infloor", false);
            controller.SetBool("endedfalling", false);
            controller.SetBool("standing", false);
            afterjump = true;
            controller.Play("jump_animation");
        }

        if (Input.GetKey(KeyCode.C) && controller.GetBool("idle1") == true)
        {
            ChangeStanceto2();//cambia a la stance 2
        }
        else
            controller.ResetTrigger("changeidle-2");

        if (Input.GetKey(KeyCode.T) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !controller.GetBool("jump") && !controller.GetBool("crouch"))
        {
            if (controller.GetBool("attack1") && controller.GetBool("attack2"))
                controller.SetBool("attack3", true);
            if (controller.GetBool("attack1") == true)
                controller.SetBool("attack2", true);
            if (controller.GetBool("attack1") == false)
                controller.Play("attack_animation_1");

            controller.SetBool("attack1", true);
            controller.SetBool("idle1", false);
        }
        else
        {
            controller.SetBool("attack1", false);
            controller.SetBool("attack2", false);
            controller.SetBool("attack3", false);
        }

        if (controller.GetBool("idle2") == true && (!controller.GetBool("attack1")))
        {
            if (change == true)
            {
                StartCoroutine(ChangetoIdle1(4));
                change = false;
            }
            if (changeidle == true)
            {
                controller.Play("sword_shte_animation");
                controller.SetBool("changeidle-1", true);
                controller.SetBool("changeidle-2", false);
                controller.SetBool("idle2", false);
                standup = true;
                controller.SetBool("changeidle-1", false);
                aux = false;
            }
        }
        else
            StopCoroutine(ChangetoIdle1(4));
        if (aux == false && change == false)
        {
            controller.SetBool("idle1", true);
            aux = true;
            change = true;
            changeidle = false; 
        }
        
        //Para cancelar las animaciones

        if (!controller.GetBool("endedfalling"))
        {
            controller.Play("after_jump_crouch_animation");
            StartCoroutine(ChangetoStanding(3));
            if (standup == false)
            {
                standup = true;
                afterjump = false;
            }
            controller.SetBool("endedfalling", false);
        }

        if (afterjump == false)
        {
            controller.SetBool("infloor", true);
            controller.SetBool("standing", true);
        }

        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.T) || controller.GetBool("standing") == false || controller.GetBool("changeidle-2") == true || controller.GetBool("changeidle-1") == true))
        {
            if (controller.GetBool("idle1") && controller.GetBool("running"))
                controller.Play("idle_animation");
            else if (controller.GetBool("idle2") && controller.GetBool("running"))
                controller.Play("idle_animation_2");
            controller.SetBool("running", false);
            controller.SetBool("crouch", false);
            controller.SetBool("jump", false);
            controller.SetBool("changeidle-1", false);
            controller.SetBool("changeidle-2", false);
            if(!controller.GetBool("idle1"))
                controller.SetBool("idle2", true);
        }

        else
            controller.runtimeAnimatorController = Resources.Load("Animator/idle") as RuntimeAnimatorController;
    }
    
    private IEnumerator ChangetoIdle1(int time)
    {
        yield return new WaitForSeconds(time);
        if (controller.GetBool("attack1") == false && controller.GetBool("running") == false)
            changeidle = !changeidle;
        //controller.SetBool("idle1", true);
    }

    private IEnumerator ChangetoStanding(int time)
    {
        yield return new WaitForSeconds(time);
        standup = false;
    }
    
    private void fixLandingAnimation()
    {

    }
}
