using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused;
    public GameObject pauseMenu;

    public PlayerMovement moveStopper;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                moveStopper.unpaused = false;
            }
            else
            {
                Time.timeScale = 1;
                gamePaused = false;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                moveStopper.unpaused = true;
            }
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }
}
