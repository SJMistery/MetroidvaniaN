using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    public GameObject openDoorSound; 
    public Vector3 movement;

    public void Start()
    {

        if (name == "DoorTopCastle")
        {
            this.transform.position = GlobalController.Instance.UpDoorPosCastle;
        }

        if (name == "DoorMidCastle")
        {
            this.transform.localPosition = GlobalController.Instance.DownDoorPosCastle;
        }

    }

    public void Move()
    {
        if (name == "DoorTopCastle" && GlobalController.Instance.doorUpActivated == false)
        {
           GlobalController.Instance.UpDoorPosCastle += movement;
           this.transform.position = GlobalController.Instance.UpDoorPosCastle;
           Instantiate(openDoorSound);
           GlobalController.Instance.doorUpActivated = true;
        }

        if (name == "DoorMidCastle" && GlobalController.Instance.doorMidActivated == false)
        {
            GlobalController.Instance.DownDoorPosCastle += movement;
            this.transform.localPosition = GlobalController.Instance.DownDoorPosCastle;
            Instantiate(openDoorSound);
            GlobalController.Instance.doorMidActivated = true;
        }
    }
}
