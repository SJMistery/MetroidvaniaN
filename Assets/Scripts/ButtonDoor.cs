using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{

    DoorsController doorMidCastle, doorTopCastle;
    private CharacterController2D_Mod cc;

    public bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
        doorMidCastle = GameObject.Find("DoorMid").GetComponent<DoorsController>(); //referencia a la puerta del ascensor del castillo.
        doorTopCastle = GameObject.Find("DoorTop").GetComponent<DoorsController>(); //referencia a puerta del boss del castillo.
    }

    private void Update()
    {
        if(GlobalController.Instance.doorUpActivated == true && activated == false)        {            if (this.gameObject.name == "TopDoorButton" && doorTopCastle.canMove)
            {
                doorTopCastle.Move();
                this.transform.Rotate(0, 0, 30);               
                activated = true;
            }
        }
        else if(GlobalController.Instance.doorMidActivated == true && activated == false)
        {
            if (this.gameObject.name == "MidDoorButton" && doorMidCastle.canMove)
            {
                doorMidCastle.Move();
                this.transform.Rotate(0, 0, 30);
                activated = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (this.gameObject.name == "MidDoorButton" && collision.gameObject.tag == "Player" && ((cc.controller && Input.GetButton("Interact MANDO")) || ((!cc.controller) && Input.GetButton("Interact"))) && doorMidCastle.canMove)
        {

                doorMidCastle.Move();
                this.transform.Rotate(0, 0, 30);
        }

        if (this.gameObject.name == "TopDoorButton" && collision.gameObject.tag == "Player" && ((cc.controller && Input.GetButtonDown("Interact MANDO")) || ((!cc.controller) && Input.GetButtonDown("Interact"))) && doorTopCastle.canMove)
        {
                doorTopCastle.Move();
                this.transform.Rotate(0, 0, 30);
        }
    }
}
