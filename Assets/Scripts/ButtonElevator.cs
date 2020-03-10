using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonElevator : MonoBehaviour
{

    public bool isMoving = false;
    public int speed;
    ElevatorController elevator;
    private CharacterController2D_Mod cc;


    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
        elevator = GameObject.Find("Elevator").GetComponent<ElevatorController>();
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && ((cc.controller && Input.GetButton("Interact MANDO")) || ((!cc.controller) && Input.GetButton("Interact"))))
        {
            
            elevator.isMoving = true;
        }
    }
}
