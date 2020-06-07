using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonElevator : MonoBehaviour
{

    public bool isMoving = false;
    public int speed;
    private bool elevatorTrigger = false;
    ElevatorController elevator;
    private CharacterController2D_Mod cc;


    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
        elevator = GameObject.Find("Elevator").GetComponent<ElevatorController>();
    }

    // Update is called once per frame

    void Update()
    {
        if (elevatorTrigger &&  Input.GetButtonDown("Interact"))
        {
            elevator.isMoving = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            elevatorTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            elevatorTrigger = false;
        }
    }

}
