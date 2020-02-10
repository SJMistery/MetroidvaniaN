using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
    }

    public void HideMenu()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
