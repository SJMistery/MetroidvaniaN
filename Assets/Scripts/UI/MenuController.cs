using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject Menu;

    public static MenuController Instance;
    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
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
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ActivateOptionMenu()
    {
        OptionScript.Instance.ActivateMenu();
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
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

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) && GlobalController.Instance.actualLevel != GlobalController.Level.TITLE)
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
        }
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().sortingLayerName = "Player";
        GetComponent<Canvas>().sortingOrder = 22;
    }
}
