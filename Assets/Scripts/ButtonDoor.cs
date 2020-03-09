using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{

    DoorsController doorMidCastle, doorTopCastle;

    public bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        doorMidCastle = GameObject.Find("DoorMid").GetComponent<DoorsController>(); //referencia a la puerta del ascensor del castillo.
        doorTopCastle = GameObject.Find("DoorTop").GetComponent<DoorsController>(); //referencia a puerta del boss del castillo.
    }

    private void Update()
    {
        if(GlobalController.Instance.doorMidActivated && activated == false)
        {
            if (this.gameObject.name == "MidDoorButton"  && doorMidCastle.canMove)
            {
                doorMidCastle.Move();
                this.transform.Rotate(0, 0, 30);
                activated = true;
            }
        }
        if (GlobalController.Instance.doorUpActivated && activated == false)
        {
            if (this.gameObject.name == "TopDoorButton" && doorTopCastle.canMove)
            {
                doorTopCastle.Move();
                this.transform.Rotate(0, 0, 30);
                activated = true;
            }
        }
        if (GlobalController.Instance.doorMidActivated && activated == true)
        {
            if (this.gameObject.name == "MidDoorButton" && !doorMidCastle.canMove)
            {
                this.transform.Rotate(0, 0, 0);
                activated = false;
            }
        }
        if (GlobalController.Instance.doorUpActivated && activated == true)
        {
            if (this.gameObject.name == "TopDoorButton" && !doorTopCastle.canMove)
            {
                this.transform.Rotate(0, 0, 0);
                activated = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (this.gameObject.name == "MidDoorButton" && collision.gameObject.tag == "Player" && Input.GetButtonDown("Interact") && doorMidCastle.canMove)
        {
            doorMidCastle.Move();
            this.transform.Rotate(0, 0, 30);
            activated = true;
        }

        if (this.gameObject.name == "TopDoorButton" && collision.gameObject.tag == "Player" && Input.GetButtonDown("Interact") && doorTopCastle.canMove)
        {
            doorTopCastle.Move();
            this.transform.Rotate(0, 0, 30);
            activated = true;
        }
    }
}
