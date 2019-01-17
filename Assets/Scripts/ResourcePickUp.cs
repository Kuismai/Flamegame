using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerGPMechanics))]

public class ResourcePickUp : MonoBehaviour
{
    //private PlayerGPMechanics gpm;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") // If the object colliding with resource pickup has the Player-tag
        {
            if (PlayerGPMechanics.playerResource >= 100)
            {
                PlayerGPMechanics.playerHealth += PlayerGPMechanics.pickUpGain;
                gameObject.SetActive(false);
                Debug.Log("Restored " + PlayerGPMechanics.pickUpGain + " health");
            }

            else if (PlayerGPMechanics.playerResource < 100)
            {
                PlayerGPMechanics.playerResource += PlayerGPMechanics.pickUpGain; // We add pickUpGain value to players resource pool and deactivate the pickup object on the level
                gameObject.SetActive(false);
                Debug.Log("Restored " + PlayerGPMechanics.pickUpGain + " resource");
            }
        }
    }

    private void Awake()
    {
        //gpm = GetComponent<PlayerGPMechanics>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
