using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonElevator : MonoBehaviour
{

    public bool isMoving = false;
    public int speed;
    ElevatorController elevator;


    // Start is called before the first frame update
    void Start()
    {
        elevator = GameObject.Find("Elevator").GetComponent<ElevatorController>();
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButton("Interact"))
        {
            
            elevator.isMoving = true;
        }
    }
}
