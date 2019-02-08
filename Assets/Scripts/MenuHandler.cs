using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    //Menu stuff:
    public GameObject mainPanel, settingsPanel, creditsPanel, pausePanel, confirmationPanel, bgTintPanel;
    private bool isGamePaused = false;

    //Audio stuff:
    public AudioMixer musicMixer, sfxMixer;
    //private float musicVol, sfxVol;

    private int activeScene;

    // Use this for initialization
    void Start () {

        //Set up audio sliders:
        //settingsPanel.SetActive(true);
        //musicVol = GameObject.Find("Music Slider").GetComponent<Slider>().value;
        //sfxVol = GameObject.Find("SFX Slider").GetComponent<Slider>().value;
        //settingsPanel.SetActive(false);

        activeScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeScene = SceneManager.GetActiveScene().buildIndex;

            if (activeScene == 0)
            {
                GoToPreviousMenu();
            }
            else if (activeScene != 0)
            {
                if(!isGamePaused)
                {
                    PauseGame();
                }
                else
                {
                    GoToPreviousMenu();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            activeScene = SceneManager.GetActiveScene().buildIndex;

            if (!isGamePaused && activeScene != 0)
            {
                PauseGame();
            }
        }
    }

    // Loads a new scene:
    public void LoadScene(int sceneNumber)
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneNumber);
    }

    // Used for correct menu panel displaying:
    public void BackButtonPress()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        if(activeScene == 0) // In Main Menu:
        {
            bgTintPanel.SetActive(true);
            mainPanel.SetActive(true);

        }
        else if (activeScene != 0) // In Game:
        {
            // Open Pause Menu Panel:
            bgTintPanel.SetActive(true);
            pausePanel.SetActive(true);
        }
    }

    public void GoToPreviousMenu()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;

        if (activeScene == 0)
        {
            if (mainPanel.activeInHierarchy)
            {
                mainPanel.SetActive(false);
                confirmationPanel.SetActive(true);
            }
            else if (!mainPanel.activeInHierarchy && !settingsPanel.activeInHierarchy && !creditsPanel.activeInHierarchy && !pausePanel.activeInHierarchy && !confirmationPanel.activeInHierarchy)
            {
                bgTintPanel.SetActive(true);
                mainPanel.SetActive(true);
            }

            else if (settingsPanel.activeInHierarchy || creditsPanel.activeInHierarchy || confirmationPanel.activeInHierarchy)
            {
                settingsPanel.SetActive(false);
                creditsPanel.SetActive(false);
                confirmationPanel.SetActive(false);
                mainPanel.SetActive(true);
            }
        }
        else if (activeScene != 0)
        {
            if (settingsPanel.activeInHierarchy)
            {
                settingsPanel.SetActive(false);
                pausePanel.SetActive(true);
            }
            else if (pausePanel.activeInHierarchy)
            {
                bgTintPanel.SetActive(false);
                pausePanel.SetActive(false);
                ResumeGame();
            }
        } 
    }

    public void PauseGame()
    {
        isGamePaused = true;
        bgTintPanel.SetActive(true);
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        bgTintPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //********** AUDIO SETTINGS STUFF **********//

    // Change master volume (global variable):
    public void changeMasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }

    // Change music volume (note: parameter must be exposed):
    public void changeMusicVolume(float newVolume)
    {
        musicMixer.SetFloat("masterVolMusic", newVolume);
    }

    // Change sfx volume (note: parameter must be exposed):
    public void changeSFXVolume(float newVolume)
    {
        sfxMixer.SetFloat("masterVolSFX", newVolume);
    }
}
