using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject WinMenu;
    public GameObject LooseMenu;

    public void Retry ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


    public void OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
        GameManager.Instance().ChangeGameState(EGameState.PAUSED);
    }

    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        GameManager.Instance().ChangeGameState(EGameState.PLAYING);
    }

    public void CLoseApplication()
    {
        Application.Quit();
    }

    public void Loose ()
    {
        Time.timeScale = 0;
        LooseMenu.SetActive(true);
    }
    public void Win ()
    {
        Time.timeScale = 0;
        WinMenu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance()._currentGameState == EGameState.PAUSED)
                OpenPauseMenu();
            else
                ClosePauseMenu();
        }
    }
}
