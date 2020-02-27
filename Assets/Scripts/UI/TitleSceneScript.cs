using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour
{
    public GameObject manager;
    [SerializeField] private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager");
    }
    public void LoadLevel1()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("IntroductionScene"));
        //Estos tres le dan los valores iniciales al jugador
        GlobalController.Instance.maxHp = 5;
        GlobalController.Instance.hp = 5;
        GlobalController.Instance.cooldown = 100;
        GlobalController.Instance.maxpotions = 3;
        GlobalController.Instance.disp_potions = 3;
        GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionOutside;
        GlobalController.Instance.fromBeginning = true;
    }

    public void LoadCreditScene()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("CreditScene"));
    }

    public void ActivateMenu()
    {
        menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
