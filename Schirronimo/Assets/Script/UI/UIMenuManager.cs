using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public static UIMenuManager s_Singleton;

    public GameObject pauseMenu;

    public GameObject mainMenu;

    public GameObject gameHUD;

    public GameObject gameOverScreen;

    public GameObject controlScreen;

    public GameObject highScoreTxt;

    public GameObject currentScoreTxt;

    public GameObject finalScoreTxt;

    public GameObject finalHighScore;

    public GameObject newRecord;

    public bool inGameScene;

    public AudioSource MenuMusic;



    private void Awake() 
    {
        if(s_Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            s_Singleton = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        inGameScene = false;
        MenuMusic.Play();
        MenuMusic.loop = true;
    }

    public void OnPlayPressed()
    {
        mainMenu.SetActive(false);
        gameHUD.SetActive(true);
        inGameScene = true;
        GameManager._instance.doOnce = false;
        MenuMusic.Stop();
        SceneManager.LoadScene("Game Scene");
    }

    public void OnReturnToMenu()
    {
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        mainMenu.SetActive(true);
        inGameScene = false;
        GameManager._instance.doOnce = true;
        MenuMusic.Play();
        SceneManager.LoadScene("Main Menu Scene");
    }

    public void OnResumeGame()
    {
        pauseMenu.SetActive(false);
        gameHUD.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnControlsPressed()
    {
        controlScreen.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnRestart()
    {
        GameManager._instance.doOnce = false;
        gameOverScreen.SetActive(false);
        GameManager._instance.GameStart();
    }

    public void OnCloseControls()
    {
        controlScreen.SetActive(false);
        mainMenu.SetActive(true);
    }
}
