using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TitleSceneScript : MonoBehaviour
{
    public GameObject manager;
    [SerializeField] public GameObject savingNotFoundText;
    [SerializeField] private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager");
        savingNotFoundText.SetActive(false);
        menu.SetActive(false);
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
        GlobalController.Instance.actualLevel = GlobalController.Level.INTRO;
        //GlobalController.Instance.actualPos = GlobalController.Instance.positionOutside;
        GlobalController.Instance.fromBeginning = true;
    }

    public void LoadCreditScene()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("CreditScene"));
    }

    public void LoadDataAndScene()
    {
        SaveSystem.LoadPlayer();

        if(SaveSystem.LoadPlayer() != null) {
            switch (GlobalController.Instance.actualLevel)
            {
                case GlobalController.Level.CAVE:
                    LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.5 Cueva"));
                    break;
                case GlobalController.Level.INSIDE:
                    LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));
                    break;
                case GlobalController.Level.OUTSIDE:
                    LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.Afueras de la Torre"));
                    break;
                case GlobalController.Level.PRISON:
                    LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
                    break;
                case GlobalController.Level.ROOF:
                    LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
                    break;
                case GlobalController.Level.STORAGE:
                    LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
                    break;
            }
            LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("CreditScene"));
        }
        else
        {
            savingNotFoundText.SetActive(true);
        }
    }

    public void ActivateMenu()
    {
        menu.SetActive(true);
    }
    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {
    }
}
