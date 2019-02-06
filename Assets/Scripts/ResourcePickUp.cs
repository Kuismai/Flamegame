using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerGPMechanics))]

public class ResourcePickUp : MonoBehaviour
{
    //private PlayerGPMechanics gpm;
    private GameObject fireFlySound;
    private AudioSource fYAudioSource;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") // If the object colliding with resource pickup has the Player-tag
        {
            if (PlayerGPMechanics.playerResource >= PlayerGPMechanics.maxResource)
            {
                if (PlayerGPMechanics.playerHealth >= PlayerGPMechanics.maxHealth)
                {
                    Debug.Log("Health and Resource full.");
                }

                else if (PlayerGPMechanics.playerHealth < PlayerGPMechanics.maxHealth)
                {
                    PlayerGPMechanics.playerHealth += PlayerGPMechanics.pickUpGain;
                    gameObject.SetActive(false);
                    Debug.Log("Restored " + PlayerGPMechanics.pickUpGain + " health");
                    AudioHandler();
                }
                
            }

            else if (PlayerGPMechanics.playerResource < 100)
            {
                PlayerGPMechanics.playerResource += PlayerGPMechanics.pickUpGain; // We add pickUpGain value to players resource pool and deactivate the pickup object on the level
                gameObject.SetActive(false);
                Debug.Log("Restored " + PlayerGPMechanics.pickUpGain + " resource");
                AudioHandler();
            }
        }
    }

    private void Awake()
    {
        //gpm = GetComponent<PlayerGPMechanics>();
    }

    void Start ()
    {
        fireFlySound = GameObject.Find("fireFlySound");
        fYAudioSource = fireFlySound.GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        
    }
    private void AudioHandler()
    {
        if (!fYAudioSource.isPlaying)
            fYAudioSource.Play();

        else if (fYAudioSource.isPlaying)
        {
            GameObject clone;
            AudioSource clonedSound;
            clone = Instantiate(fireFlySound);
            clonedSound = clone.GetComponent<AudioSource>();
            clonedSound.Play();
            if (clonedSound.isPlaying)
            {
                GameObject clone1;
                AudioSource clonedSound1;
                clone1 = Instantiate(fireFlySound);
                clonedSound1 = clone.GetComponent<AudioSource>();
                clonedSound1.Play();
                //if (!clonedSound1.isPlaying)
                  //  Object.Destroy(clone1, 0.0f);
            }
           /* else if (!fYAudioSource.isPlaying || !clonedSound.isPlaying)
            {
                Object.Destroy(clone, 0.0f);
            }*/

        }

        else
            return;
    }
}
