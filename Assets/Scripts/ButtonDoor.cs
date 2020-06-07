using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{

    public DoorsController doorMidCastle, doorTopCastle, doorPuzzle, doorEndPrisson;
    private CharacterController2D_Mod cc;
    private bool midDoorTrigger = false;
    private bool topDoorTrigger = false;
    private bool puzzleDoorTrigger = false;
    private bool prisonDoorTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
        if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE)
        {
            doorMidCastle = GameObject.Find("DoorMidCastle").GetComponent<DoorsController>(); //referencia a la puerta del ascensor del castillo.
            doorTopCastle = GameObject.Find("DoorTopCastle").GetComponent<DoorsController>(); //referencia a puerta del boss del castillo.
        }
        if (GlobalController.Instance.actualLevel == GlobalController.Level.PRISON)
        {
            doorPuzzle = GameObject.Find("DoorPuzzle").GetComponent<DoorsController>();       //referencia  a la puerta de dentro de la prision, del puzle.
            doorEndPrisson = GameObject.Find("DoorEndPrison").GetComponent<DoorsController>();//refenrecia a la puerta de dentro de la prision, para salir de ella.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.name == "MidDoorButton" && collision.gameObject.tag == "Player")
        {
            midDoorTrigger = true;
        }
        if (this.gameObject.name == "TopDoorButton" && collision.gameObject.tag == "Player")
        {
            topDoorTrigger = true;
        }
        if (this.gameObject.name == "PuzzleButton" && collision.gameObject.tag == "Player")
        {
            puzzleDoorTrigger = true;
        }
        if (this.gameObject.name == "PrisonDoorButton" && collision.gameObject.tag == "Player")
        {
            prisonDoorTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.name == "MidDorrButton" && collision.gameObject.tag == "Player")
        {
            midDoorTrigger = false;
        }
        if (this.gameObject.name == "TopDoorButton" && collision.gameObject.tag == "Player")
        {
            topDoorTrigger = false;
        }
        if (this.gameObject.name == "PuzzleButton" && collision.gameObject.tag == "Player")
        {
            puzzleDoorTrigger = false;
        }
        if (this.gameObject.name == "PrisonDoorButton" && collision.gameObject.tag == "Player")
        {
            prisonDoorTrigger = false;
        }
    }

    private void Update()
    {
        if (midDoorTrigger && Input.GetButtonDown("Interact"))
        {
            doorMidCastle.Move();
            this.transform.Rotate(0, 0, 30);
        }
        if (topDoorTrigger && Input.GetButtonDown("Interact"))
        {
            doorTopCastle.Move();
            this.transform.Rotate(0, 0, 30);
        }
        if (puzzleDoorTrigger && Input.GetButtonDown("Interact"))
        {
            doorPuzzle.Move();
            this.transform.Rotate(0, 0, 30);
        }
        if (prisonDoorTrigger && Input.GetButtonDown("Interact"))
        {
            doorEndPrisson.Move();
            this.transform.Rotate(0, 0, 30);
        }
    }
}
