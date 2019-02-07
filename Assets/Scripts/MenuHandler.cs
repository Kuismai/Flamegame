using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    //Menu stuff:
    public GameObject mainPanel, settingsPanel, creditsPanel, pausePanel, confirmationPanel;
    private bool isGamePaused = false;

    //Audio stuff:
    public AudioMixer musicMixer, sfxMixer;
    private float musicVol, sfxVol;

    private int activeScene;

    // Use this for initialization
    void Start () {

        //Set up audio sliders:
        settingsPanel.SetActive(true);
        musicVol = GameObject.Find("Music Slider").GetComponent<Slider>().value;
        sfxVol = GameObject.Find("SFX Slider").GetComponent<Slider>().value;
        settingsPanel.SetActive(false);

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

        Debug.Log("Loading scene " + sceneNumber + "...");
        isGamePaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneNumber);
    }

    // Used for correct menu panel displaying:
    public void BackButtonPress()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        if(activeScene == 0) //In Main Menu
        {
            mainPanel.SetActive(true);

        }
        else //In Game
        {
            // Open Pause Menu Panel:
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
                pausePanel.SetActive(false);
                ResumeGame();
            }
        } 
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        isGamePaused = true;
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming Game...");
        isGamePaused = false;
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

    }

    //********** AUDIO SETTINGS STUFF **********//

    // Change master volume (global variable):
    public void changeMasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
        Debug.Log(AudioListener.volume);
    }

    // Change music volume (note: parameter must be exposed):
    public void changeMusicVolume(float newVolume)
    {
        musicMixer.SetFloat("masterVolMusic", newVolume);
        //Debug.Log(musicVol);
    }

    // Change sfx volume (note: parameter must be exposed):
    public void changeSFXVolume(float newVolume)
    {
        musicMixer.SetFloat("masterVolSFX", newVolume);
        //Debug.Log(sfxVol);
    }
}
