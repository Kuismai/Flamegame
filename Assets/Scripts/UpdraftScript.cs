using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdraftScript : MonoBehaviour {
    
    private Bonfire bonfire;

	// Use this for initialization
	void Start () {
        bonfire = gameObject.GetComponentInParent<Bonfire>();
	}

    // When the player is in the updraft area:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bonfire.isLit)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerGPMechanics.updrafting = true;
            }
        }
    }

    // When the player leaves the updraft area:
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (bonfire.isLit)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerGPMechanics.updrafting = false;
            }
        }
    }
}