using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerGPMechanics : MonoBehaviour
{
    [SerializeField] static float startingHealth = 100f; // At how much health you start with
    [SerializeField] static float startingResource = 0f; // At how much resource you start with
    [SerializeField] float healthDecayMult = 1f; // Multiplier for how fast health decays while not in safe zone
    [SerializeField] float drainDelay = 5f; // Delay between resource drains to restore health
    [SerializeField] float hpThreshold = 25f; // At what health total we start draining resource for health
    [SerializeField] float resourceDrain = 5f; // How much resource is converted into health at once
    [SerializeField] float healthRestoreMult = 5f; // Multiplier for how much health we gain per second from a safe zone
    [SerializeField] float overheatMult = 2f; // Multiplier for how much health is drained because of Overheat. 2 = twice as much health drain etc.
    [SerializeField] float resourceBurnRate = 0.5f; // Multiplier for how much less/more your resource drains while using Overheat
    [SerializeField] float overheatRegenMult = 0.2f; // How much health we regen while Overheating using resource
    [SerializeField] bool decayHealthInOverheat = false;
    [SerializeField] public static float pickUpGain = 10f; // How much resource we gain from a single pickup

    public static float playerHealth = 100f;
    public static float playerResource = 0f;
    public static float maxResource = 100f;
    public static bool playerDead = false;
    public static bool atSafeZone = false;
    public static bool overheatActive = false;

    //private Rigidbody2D rb;
    //private bool jumping;

    public GameObject overheatHitbox;
    public Text resourceUI;
    public Text healthUI;

    private float drainTimer = 0;



    void Start()
    {
        playerHealth = startingHealth;
        playerResource = startingResource;
        overheatHitbox = GameObject.Find("OverheatHitbox");
        //resourceUI =  .Find("health");
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButton("Fire3")) // Here we're "listening" if the player activates Overheat. Press and keep <Shift> down to keep Overheat active
        {
            overheatActive = true;
            overheatHitbox.SetActive(true);
        }

        else if (!Input.GetButton("Fire3")) // If <Shift> is released, Overheat is deactivated
        {
            overheatActive = false;
            overheatHitbox.SetActive(false);
        }

        resourceUI.text = "Resource: " + Mathf.RoundToInt(playerResource); // Updating UI for Health and Resource values
        healthUI.text = "HP: " + Mathf.RoundToInt(playerHealth);
    }

    void FixedUpdate()
    {
        if (!atSafeZone)
        {
            if (!overheatActive) // If Overheat is not active, we'll drain health normally
            {
                playerHealth -= Time.deltaTime * healthDecayMult;
            }

            else if (overheatActive) // If Overheat is active, we'll drain resource first
            {
                if (decayHealthInOverheat)
                {
                    playerHealth -= Time.deltaTime * healthDecayMult;
                }

                if (playerResource > 0)
                {
                    playerResource -= Time.deltaTime * overheatMult * resourceBurnRate;
                    playerHealth += Time.deltaTime * overheatRegenMult;
                }

                else if (playerResource <= 0) // If resource drops to zero, we'll start draining health to maintain Overheat
                {
                    playerHealth -= Time.deltaTime * overheatMult;
                }

            }

            //Debug.Log("HP: " + playerHealth);
            //Debug.Log("Resource: " + playerResource);

            if (playerHealth < hpThreshold) // If player health drops below Threshold, we keep checking if player is dead (player health at or below Zero) 
            {
                if (playerHealth <= 0)
                {
                    playerDead = true;
                    //Debug.Log("Your fire went out. Ripperoni pepperoni.");
                }

                drainTimer += Time.deltaTime;
                // Sumtin' fishy right here boiii

                if (drainTimer >= drainDelay && playerResource > 0) // Here we drain resource every drainDelay (default 5 seconds) and add the same amount to health, based on determined resourceDrain variable (Default is 5)
                {
                    playerHealth += resourceDrain;
                    playerResource -= resourceDrain;

                    drainTimer = 0;

                    // Debug.Log("Drained " + resourceDrain + " resource to restore health");
                }
            }
        }

        else if (atSafeZone) // If player is at safe zone, we ignore health decay and conversions
        {
            if (playerHealth > startingHealth) // If player health is over starting health, we set playerHealth to startingHealth
            {
                playerHealth = startingHealth;
            }

            else if (playerHealth < startingHealth) // If player health is under starting health, we restore player health with by healthRestoreMult every second (SafeZone = Healing zone)
            {
                playerHealth += Time.deltaTime * healthRestoreMult;
            }
        }

        if (playerResource > maxResource)
        {
            playerResource = maxResource;
        }

        if (playerResource < 0)
        {
            playerResource = 0;
        }

        if (playerDead)
        {
            ResetPlayer();
            Debug.Log("You're dead dawg");
        }

    }

    public static void ResetPlayer()
    {
        GameObject player = GameObject.Find("Player");

        PlayerGPMechanics.playerHealth = PlayerGPMechanics.startingHealth;
        PlayerGPMechanics.playerResource = PlayerGPMechanics.startingResource;

        player.transform.position = CheckpointHandler.lastCheckpoint;
    }
}
