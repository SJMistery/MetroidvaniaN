using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;
    public GameObject confirmattionPanel;

    public GameObject player;

    [SerializeField] private TextMeshProUGUI potionText;
    [SerializeField] private bool pageChanged = false;
    [SerializeField] private bool menuON = false;
    [SerializeField] private bool changingStateMenu = false;

    public static MenuController Instance;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        firstPage.SetActive(true);
        secondPage.SetActive(false);
        thirdPage.SetActive(false);
        confirmattionPanel.SetActive(false);
    }
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void HideMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        firstPage.SetActive(true);
        secondPage.SetActive(false);
        thirdPage.SetActive(false);
    }
    public void LoadTitleScene()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("TitleScene"));
    }

    public void ChangePage()
    {
        if (!pageChanged && firstPage.activeSelf)
        {
            firstPage.SetActive(false);
            secondPage.SetActive(true);
            thirdPage.SetActive(false);
            pageChanged = true;
        } 
        else if (!pageChanged && secondPage.activeSelf)
        {
            secondPage.SetActive(false);
            firstPage.SetActive(false);
            thirdPage.SetActive(true);
            pageChanged = true;
        }
        else if(!pageChanged && thirdPage.activeSelf)
        {
            secondPage.SetActive(false);
            firstPage.SetActive(true);
            thirdPage.SetActive(false);
            pageChanged = true;
        }
        pageChanged = false;
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(player.GetComponent<CharacterController2D_Mod>());
    }

    public void SaveandReturn()
    {
        SaveSystem.SavePlayer(player.GetComponent<CharacterController2D_Mod>());
        if (GlobalController.Instance.streamEnded != false)
            LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("TitleScene"));
    }

    public void SaveandQuit()
    {
        SaveSystem.SavePlayer(player.GetComponent<CharacterController2D_Mod>());
        Application.Quit();
    }

    public void ShowConfirmationPanel()
    {
        confirmattionPanel.SetActive(true);
    }

    public void HideConfirmationPanel()
    {
        confirmattionPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().sortingLayerName = "Player";
        GetComponent<Canvas>().sortingOrder = 21;
        potionText.text = GlobalController.Instance.disp_potions.ToString();
        if(!changingStateMenu)
        {
            if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                && (GlobalController.Instance.actualLevel != GlobalController.Level.TITLE 
                || GlobalController.Instance.actualLevel != GlobalController.Level.INTRO
                || GlobalController.Instance.actualLevel != GlobalController.Level.CREDIT))
            {
                if (!menuON)
                {
                    menu.SetActive(true);
                    Time.timeScale = 0;
                    menuON = true;
                }
                else
                {
                    menu.SetActive(false);
                    Time.timeScale = 1;
                    menuON = false;
                    firstPage.SetActive(true);
                    secondPage.SetActive(false);
                    thirdPage.SetActive(false);
                    confirmattionPanel.SetActive(false);
                }
                changingStateMenu = true;
            }
        }
        if(!((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))))
        {
            changingStateMenu = false;
        }

        if (GlobalController.Instance.actualLevel != GlobalController.Level.CREDIT ||
            GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE ||
            GlobalController.Instance.actualLevel != GlobalController.Level.TITLE)
            player = GameObject.FindGameObjectWithTag("Player");

        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().sortingLayerName = "Player";
        GetComponent<Canvas>().sortingOrder = 22;
    }
}
