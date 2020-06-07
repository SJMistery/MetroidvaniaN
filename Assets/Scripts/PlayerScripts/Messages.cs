using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{

    private GameObject interactMessage;
    private GameObject lookMessage;
    private GameObject ladderMessage;
    private GameObject platformMessage;
    ElevatorController elevator;
    // Start is called before the first frame update
    void Start()
    {
        interactMessage = GameObject.Find("Press 'E' To interact.");
        interactMessage.SetActive(false);
        lookMessage = GameObject.Find("ControlCameraMessage");
        lookMessage.SetActive(false);

        if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE)
        {
            elevator = GameObject.Find("Elevator").GetComponent<ElevatorController>();
            ladderMessage = GameObject.Find("PressW/S to climb the ladder");
            platformMessage = GameObject.Find("Press S to drop from tiny platforms");
            ladderMessage.SetActive(false);
            platformMessage.SetActive(false);
        }

    }
    private void Update()
    {
        if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE && elevator.isMoving)
        {
            interactMessage.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Roof")
        {
            interactMessage.SetActive(true);
        }
        if (collision.name == "MessagePoint")
        {
            lookMessage.SetActive(true);
        }
        if (collision.tag == "ElevatorMessage")
        {
            interactMessage.SetActive(true);
        }
        if (collision.name == "TopDoorButton" && GlobalController.Instance.doorUpActivated == false)
        {
            interactMessage.SetActive(true);
        }
        else if (collision.name == "TopDoorButton" && GlobalController.Instance.doorUpActivated == true)
        {
            interactMessage.SetActive(false);
        }

        if (collision.name == "MidDoorButton" && GlobalController.Instance.doorMidActivated == false)
        {
            interactMessage.SetActive(true);
        }
        else if (collision.name == "MidDoorButton" && GlobalController.Instance.doorMidActivated == true)
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "PuzzleButton" && GlobalController.Instance.doorPuzzleActivated == false)
        {
            interactMessage.SetActive(true);
        }
        else if (collision.name == "PuzzleButton" && GlobalController.Instance.doorPuzzleActivated == true)
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "PrisonDoorButton" && GlobalController.Instance.doorPrisonActivated == false)
        {
            interactMessage.SetActive(true);
        }
        else if (collision.name == "PrisonDoorButton" && GlobalController.Instance.doorPrisonActivated == true)
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "AbysmFallButton" && GlobalController.Instance.abyssOpened == false)
        {
            interactMessage.SetActive(true);
        }
        else if (collision.name == "AbysmFallButton" && GlobalController.Instance.abyssOpened == true)
        {
            interactMessage.SetActive(false);
        }

        if (collision.tag == "Elevator")
        {
            interactMessage.SetActive(true);
        }

        if(collision.gameObject.name == "LadderTutorial" && !GlobalController.Instance.ladderTutorialDone)
        {
            ladderMessage.SetActive(true);
        }
        if(collision.gameObject.name == "PlatformTutorial" && !GlobalController.Instance.platformTutorialDone)
        {
            platformMessage.SetActive(true);
        }
        if(collision.tag == "Savepoint")
        {
            interactMessage.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Roof")
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "MessagePoint")
        {
            lookMessage.SetActive(false);
        }
        if (collision.tag == "ElevatorMessage")
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "TopDoorButton")
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "MidDoorButton")
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "PrisonDoorButton")
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "PuzzleButton")
        {
            interactMessage.SetActive(false);
        }
        if (collision.name == "AbysmFallButton")
        {
            interactMessage.SetActive(false);
        }
        if (collision.tag == "Elevator" )
        {
            interactMessage.SetActive(false);
        }
        if (collision.gameObject.name == "LadderTutorial" )
        {
            ladderMessage.SetActive(false);
            GlobalController.Instance.ladderTutorialDone = true; ;       
        }
        if (collision.gameObject.name == "PlatformTutorial")
        {
            platformMessage.SetActive(false);
            GlobalController.Instance.platformTutorialDone = true;
        }
        if (collision.tag == "Savepoint")
        {
            interactMessage.SetActive(false);
        }
    }
}
