using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickUp : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            PlayerGPMechanics.playerResource += PlayerGPMechanics.pickUpGain;
            gameObject.SetActive(false);
            Debug.Log("Resource now at: " + PlayerGPMechanics.playerResource);
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
