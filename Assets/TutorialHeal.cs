using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHeal : MonoBehaviour
{
    private bool tutorialHealShowed = false;
    public GameObject tutorialHeal;
    // Start is called before the first frame update
    void Start()
    {
        tutorialHeal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialHeal == true)
        {
            if (!GlobalController.Instance.tutorialHealDone && Input.GetButtonDown("Heal"))
            {
                tutorialHeal.SetActive(false);
                GlobalController.Instance.tutorialHealDone = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !tutorialHealShowed && !GlobalController.Instance.tutorialHealDone)
        {
            tutorialHeal.SetActive(true);
            tutorialHealShowed = true;
        }
    }
}
