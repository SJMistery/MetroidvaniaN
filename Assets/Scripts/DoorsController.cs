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
        if (name == "DoorPuzzle")
        {
            this.transform.localPosition = GlobalController.Instance.PuzzleDoorPosPrison;
        }

        if (name == "DoorEndPrison")
        {
            this.transform.localPosition = GlobalController.Instance.PrisonDoorPos;
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
        if (name == "DoorPuzzle" && GlobalController.Instance.doorPuzzleActivated == false)
        {
            GlobalController.Instance.PuzzleDoorPosPrison += movement;
            this.transform.localPosition = GlobalController.Instance.PuzzleDoorPosPrison;
            Instantiate(openDoorSound);
            GlobalController.Instance.doorPuzzleActivated = true;
        }
        if (name == "DoorEndPrison" && GlobalController.Instance.doorPrisonActivated == false)
        {
            GlobalController.Instance.PrisonDoorPos += movement;
            this.transform.localPosition = GlobalController.Instance.PrisonDoorPos;
            Instantiate(openDoorSound);
            GlobalController.Instance.doorPrisonActivated = true;
        }
    }
}
