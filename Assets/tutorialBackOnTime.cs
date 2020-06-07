using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialBackOnTime : MonoBehaviour
{
    public GameObject tutorialBTJump;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reverser"))
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
            StartCoroutine(waitToSpawnDestroy(2f));
        }
    }
    IEnumerator waitToSpawnDestroy(float time)
    {
        //The attack animation runs from AnimationState()
        //Esperar un breve periodo de tiempo antes de que salte el codigo, i hacer desaparecer al enemigo.
        yield return new WaitForSeconds(time);
        Instantiate(tutorialBTJump);
        Destroy(this.gameObject);
    }
}
