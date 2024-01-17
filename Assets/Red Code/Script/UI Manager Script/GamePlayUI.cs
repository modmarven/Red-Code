using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public GameObject gamePause;
    public GameObject gameOver;
    
    void Start()
    {
        gamePause.SetActive(false);
        gameOver.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        gamePause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_A");
    }

    public void Controll()
    {
        SceneManager.LoadScene("Controll");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
