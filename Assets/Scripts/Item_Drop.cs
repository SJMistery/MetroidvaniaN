using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    public bool taken = false;
    private InverseTime inverseTime;
    private CharacterController2D_Mod cc;
    private GameObject sombra;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetButton("Interact"))
        {
            GlobalController.Instance.inverseTimeActive = true;
        }
    }
}
