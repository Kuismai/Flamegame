using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGPMechanics : MonoBehaviour
{
    public static float playerHealth = 100f;
    public static float playerResource = 0f;
    public static bool playerDead = false;
    public static bool atSafeZone = false;

	void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
		if (!atSafeZone)
        {
            playerHealth -= Time.deltaTime;
            Debug.Log("HP: " + playerHealth);
            Debug.Log("Resource: " + playerResource);
        }

        if (playerHealth < 25)
        {
            if (playerResource <= 0 && playerHealth <= 0)
            {
                playerDead = true;
            }

            playerHealth += 5 * Time.deltaTime;
            playerResource -= 5 * Time.deltaTime;

        }
	}
}
