using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickUp : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") // If the object colliding with resource pickup has the Player-tag
        {
            PlayerGPMechanics.playerResource += PlayerGPMechanics.pickUpGain; // We add pickUpGain value to players resource pool and deactivate the pickup object on the level
            gameObject.SetActive(false);
            //Debug.Log("Resource now at: " + PlayerGPMechanics.playerResource);
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
