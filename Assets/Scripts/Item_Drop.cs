using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    public bool taken = false;

    public GameObject message;

    public GameObject createPlatformSound;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetButton("Interact"))
        {
            GlobalController.Instance.inverseTimeActive = true;
            GlobalController.Instance.bossPlatformSpawned = true;
            Instantiate(message);
            Instantiate(createPlatformSound);
            this.gameObject.SetActive(false);
        }
    }
}
