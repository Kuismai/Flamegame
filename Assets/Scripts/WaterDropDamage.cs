using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropDamage : MonoBehaviour
{
    [SerializeField] float dropletDamage = 40f;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") // If the colliding object has the Player-tag
        {
            PlayerGPMechanics.playerHealth -= dropletDamage;
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "DropletExit")
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
