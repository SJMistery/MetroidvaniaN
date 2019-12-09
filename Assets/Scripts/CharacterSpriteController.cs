using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    [SerializeField]private Animator controller;
    [SerializeField] private bool animationState;
    [SerializeField] private bool attack2;
    [SerializeField] private bool attack3;
    [SerializeField] private int attackCount;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInChildren<Animator>();
        attack2 = false;
        attack3 = false;
        attackCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //controller.SetTrigger("running");
        
        animationState = controller.IsInTransition(0);
        if (Input.GetKey(KeyCode.D))
            controller.SetTrigger("running");
        else
            controller.ResetTrigger("running");

        if (Input.GetKey(KeyCode.S))
            controller.SetTrigger("crouch");
        else
            controller.ResetTrigger("crouch");

        if (Input.GetKey(KeyCode.W))
            controller.SetTrigger("jump");
        else
            controller.ResetTrigger("jump");

        if (Input.GetKey(KeyCode.C))
        {
            controller.SetTrigger("changeidle-1");
        }
        else
            controller.ResetTrigger("changeidle-1");

        if (Input.GetKey(KeyCode.T))
         {
                controller.SetTrigger("attack1");
        }
        //else
        //    controller.runtimeAnimatorController = Resources.Load("Animator/idle") as RuntimeAnimatorController;
    }
}
