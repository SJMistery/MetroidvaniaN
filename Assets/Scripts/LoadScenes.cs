using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScenes : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("0.Afueras de la Torre");
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
