using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{

    public DoorsController doorMidCastle, doorTopCastle;
    private CharacterController2D_Mod cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
        if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE)
        {
            doorMidCastle = GameObject.Find("DoorMidCastle").GetComponent<DoorsController>(); //referencia a la puerta del ascensor del castillo.
            doorTopCastle = GameObject.Find("DoorTopCastle").GetComponent<DoorsController>(); //referencia a puerta del boss del castillo.
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (this.gameObject.name == "MidDoorButton" && collision.gameObject.tag == "Player" && ((cc.controller && Input.GetButton("Interact MANDO")) || ((!cc.controller) && Input.GetButton("Interact"))) && !GlobalController.Instance.doorMidActivated)
        {
                doorMidCastle.Move();
                this.transform.Rotate(0, 0, 30);
        }

        if (this.gameObject.name == "TopDoorButton" && collision.gameObject.tag == "Player" && ((cc.controller && Input.GetButtonDown("Interact MANDO")) || ((!cc.controller) && Input.GetButtonDown("Interact"))) && !GlobalController.Instance.doorUpActivated)
        {
                doorTopCastle.Move();
                this.transform.Rotate(0, 0, 30);
        }
    }
}
