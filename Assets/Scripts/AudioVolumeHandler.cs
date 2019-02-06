using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeHandler : MonoBehaviour {
    private float masterVolume, musicVolume, sfxVolume;

	// Use this for initialization
	void Start () {
        masterVolume = AudioListener.volume;
        // musicVolume = mixerSongs?
        // sfxVolume = ? mixerSFX?
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void changeVolume(string volumeSliderName)
    {
        switch (volumeSliderName)
        {
            case "Master":
                //Change master volume:
                Debug.Log("LOL");
                break;
            case "Music":
                //Change music volume:
                Debug.Log("ROFL");
                break;
            case "SFX":
                //Change sfx volume:
                Debug.Log("LMAO");
                break;
            default:
                // Do nothing
                break;
        }
    }
}
