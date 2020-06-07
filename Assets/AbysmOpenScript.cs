using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbysmOpenScript : MonoBehaviour
{
    public GameObject openAbyssSound;
    private GameObject abyss;
    public GameObject skeleton;
    private bool doorOpened = false;
    private bool abysmDoorTrigger = false;
    // Start is called before the first frame update

    private void Start()
    {
        abyss = GameObject.Find("Secretos");
        if (GlobalController.Instance.abyssOpened)
        {
            abyss.SetActive(false);
        }
        else
        {
            abyss.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            abysmDoorTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            abysmDoorTrigger = false;
        }
    }
    private void Update()
    {
        if (GlobalController.Instance.abyssOpened == false)
        {
            if (Input.GetButtonDown("Interact") && !doorOpened && abysmDoorTrigger)
            {
                Instantiate(openAbyssSound);
                abyss.SetActive(false);
                Instantiate(skeleton, new Vector3(-12.54f, -66.78f, 0), Quaternion.identity);
                doorOpened = true;
                this.transform.Rotate(0, 0, 30);
                GlobalController.Instance.abyssOpened = true;
            }
        }
    }
}
