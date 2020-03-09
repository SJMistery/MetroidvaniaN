using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DoorsController : MonoBehaviour
{

    public Vector3 movement;
    public bool canMove = true;

    public void Start()
    {
        if (name == "DoorTop")
            transform.position = GlobalController.Instance.UpDoorPos;
        else if(name == "DoorMid")
            transform.position = GlobalController.Instance.DownDoorPos;

    }

    // Start is called before the first frame update
    public void Update()
    {
        if (name == "DoorTop")
        {
            if (GlobalController.Instance.doorUpActivated == false)
                transform.position = new Vector3(0, 0);
        }
        else if (name == "DoorMid")
        {
            if (GlobalController.Instance.doorMidActivated == false)
                transform.position = new Vector3(0, 0);
        }
    }

    public void Move()
    {
        if (canMove)
        {
            this.transform.position += movement;
            canMove = false;
            if (name == "DoorTop")
                GlobalController.Instance.UpDoorPos = transform.position;
            else if (name == "DoorMid")
                GlobalController.Instance.DownDoorPos = transform.position;

            if (name == "DoorTop")
                GlobalController.Instance.doorUpActivated = true;
            else if (name == "DoorMid")
               GlobalController.Instance.doorMidActivated = true;
        }
    }
}
