using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static bool GameIsPaused;
    public GameObject PauseMenuUI, ScoreUI;
    public GameManager gM;

    void Awake()
    {
        gM = FindObjectOfType<GameManager>();
        PauseMenuUI.SetActive(false);
        //ResumeGame();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("JoyMenu1"))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else if(!GameIsPaused)
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        gM.playerFreeze = true;
        AudioManager.instance.SetMasterVolume(-40);
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gM.playerFreeze = false;
        AudioManager.instance.SetMasterVolume(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}

