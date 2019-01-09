using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerGPMechanics : MonoBehaviour
{
    [SerializeField] float startingHealth = 100f; // At how much health you start with
    [SerializeField] float startingResource = 0f; // At how much resource you start with
    [SerializeField] float drainDelay = 5f; // Delay between resource drains to restore health
    [SerializeField] float hpThreshold = 25f; // At what health total we start draining resource for health
    [SerializeField] float resourceDrain = 5f; // How much resource is converted into health at once
    [SerializeField] float healthRestoreMult = 5f; // Multiplier for how much health we gain per second from a safe zone
    [SerializeField] float overheatMult = 2f; // Multiplier for how much health is drained because of Overheat. 2 = twice as much health drain etc.

    [SerializeField] public static float pickUpGain = 10f; // How much resource we gain from a single pickup

    public static float playerHealth = 100f;
    public static float playerResource = 0f;
    public static bool playerDead = false;
    public static bool atSafeZone = false;
    public static bool overheatActive = false;

    public GameObject overheatHitbox;
    public Text resourceUI;
    public Text healthUI;

    private float drainTimer = 0;

     

	void Start ()
    {
        playerHealth = startingHealth;
        playerResource = startingResource;
        overheatHitbox = GameObject.Find("OverheatHitbox");
        //resourceUI =  .Find("health");
    }

    void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            overheatActive = true;
            overheatHitbox.SetActive(true);
        }

        else if (!Input.GetButton("Fire3"))
        {
            overheatActive = false;
            overheatHitbox.SetActive(false);
        }

        resourceUI.text = "Resource: " + playerResource;
        healthUI.text = "HP: " + playerHealth;
    }

    void FixedUpdate ()
    {
		if (!atSafeZone)
        {
            if (!overheatActive)
            {
                playerHealth -= Time.deltaTime;
            }

            else if (overheatActive)
            {
                playerHealth -= Time.deltaTime * overheatMult;
            }

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
                // Sumtin' fishy right here boiii

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
                playerHealth += Time.deltaTime * healthRestoreMult;
            }
        }
        
	}
}
