using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveAttack: MonoBehaviour
{
    public GameObject tutorialMove;
    public GameObject tutorialAttack;
    private bool showTutorialMove = false;
    private bool showTutorialAttack = false;
    // Start is called before the first frame update

    private void Start()
    {
        tutorialMove.SetActive(false);
        tutorialAttack.SetActive(false);
    }
    private void Update()
    {
        //Tutorial MOVE
        if (!showTutorialMove)
        {
            if (GlobalController.Instance.firstCutsceneEnded == true && !GlobalController.Instance.tutorialMoveDone)
            {
                tutorialMove.SetActive(true);
                showTutorialMove = true;
            }
        }
        if (GlobalController.Instance.firstCutsceneEnded == true && showTutorialMove && Input.GetButtonDown("Jump"))
        {
            tutorialMove.SetActive(false);
            GlobalController.Instance.tutorialMoveDone = true;
        }


        //Tutorial ATTACK
        if (!showTutorialAttack )
        {
            if (GlobalController.Instance.tutorialMoveDone == true && !GlobalController.Instance.tutorialAttackDone)
            {
                tutorialAttack.SetActive(true);
                showTutorialAttack = true;
            }
        }
        if (GlobalController.Instance.tutorialMoveDone == true && showTutorialAttack && Input.GetButtonDown("Attack"))
        {
            tutorialAttack.SetActive(false);
            GlobalController.Instance.tutorialAttackDone = true;
        }

    }    

}
