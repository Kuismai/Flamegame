using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    //Menu stuff:
    public GameObject mainMenuPanel, pauseMenuPanel;
    private bool isGamePaused = false;

    //Audio stuff:
    public AudioMixer musicMixer, sfxMixer;
    private float musicVol, sfxVol;

    // Use this for initialization
    void Start () {
        musicVol = GameObject.Find("Music Slider").GetComponent<Slider>().value;
        sfxVol = GameObject.Find("SFX Slider").GetComponent<Slider>().value;
    }

    private void Update()
    {
        //Pause Game when in game:
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (!isGamePaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
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
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if(activeScene == 0) //In Main Menu
        {
            // Open Main Menu Panel:
            mainMenuPanel.SetActive(true);

        }
        else //In Game
        {
            // Open Pause Menu Panel:
            pauseMenuPanel.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        isGamePaused = true;
        Time.timeScale = 0.0f;
        pauseMenuPanel.SetActive(true);
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
