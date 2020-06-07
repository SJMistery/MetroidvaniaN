using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialBT_Mod : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Reverser"))
        {
            Destroy(this.gameObject);
        }

    }
}
