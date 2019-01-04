using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGPMechanics : MonoBehaviour
{
    [SerializeField] float startingHealth = 100f; // At how much health you start with
    [SerializeField] float startingResource = 0f; // At how much resource you start with
    [SerializeField] float drainDelay = 5f; // Delay between resource drains to restore health
    [SerializeField] float hpThreshold = 25f; // At what health total we start draining resource for health
    [SerializeField] float resourceDrain = 5f; // How much resource is converted into health at once
    [SerializeField] float healthRestoreMult = 5f; // Multiplier for how much health we gain per second from a safe zone
    [SerializeField] float overheatMult = 2f; // Multiplier for how much health is drained because of Overheat

    [SerializeField] public static float pickUpGain = 10f; // How much resource we gain from a single pickup

    public static float playerHealth = 100f;
    public static float playerResource = 0f;
    public static bool playerDead = false;
    public static bool atSafeZone = false;
    public static bool overheatActive = false;

    private float drainTimer;

     

	void Start ()
    {
        playerHealth = startingHealth;
        playerResource = startingResource;
	}

    void Update()
    {
        if (overheatActive)
        {
            playerHealth -= Time.deltaTime * overheatMult;
        }
    }

    void FixedUpdate ()
    {
		if (!atSafeZone)
        {
            playerHealth -= Time.deltaTime;
            //Debug.Log("HP: " + playerHealth);
            //Debug.Log("Resource: " + playerResource);

            if (playerHealth < hpThreshold)
            {
                if (playerHealth <= 0)
                {
                    playerDead = true;
                    Debug.Log("Your fire went out. Ripperoni pepperoni.");
                }

                drainTimer += Time.deltaTime;

                if (drainTimer >= drainDelay && playerResource > 0)
                {
                    playerHealth += resourceDrain;
                    playerResource -= resourceDrain;

                    drainTimer = 0;

                    Debug.Log("Drained " + resourceDrain + " resource to restore health");
                }
            }
        }

        else if (atSafeZone && playerHealth < startingHealth)
        {
            if (playerHealth >= startingHealth)
            {
                playerHealth = startingHealth;
            }

            else
            {
                playerHealth += Time.deltaTime * 5;
            }
        }
        
	}
}
