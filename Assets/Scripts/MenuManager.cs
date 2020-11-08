using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static bool GameIsPaused;
    public GameObject PauseMenuUI, ScoreUI;
    public string mainMenuScene;
    public GameManager gM;
    public float currentMasterVolume;

    void Awake()
    {
        gM = FindObjectOfType<GameManager>();
        PauseMenuUI.SetActive(false);
        //ResumeGame();
    }


    private void Update()
    {
        currentMasterVolume = GetMasterLevel();
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
        AudioManager.instance.SetMasterVolume(GetMasterLevel()/.5f);
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gM.playerFreeze = false;
        AudioManager.instance.SetMasterVolume(currentMasterVolume /= 2f);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1;
        AudioManager.instance.SetMasterVolume(currentMasterVolume = -19);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public float GetMasterLevel()
    {
        float value;
        bool result = AudioManager.instance.aMixer.GetFloat("MasterVol", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
}

