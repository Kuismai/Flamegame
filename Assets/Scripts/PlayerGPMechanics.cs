using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PlayerGPMechanics : MonoBehaviour
{
    [SerializeField] float startingHealth = 100f; // At how much health you start with
    [SerializeField] float startingResource = 0f; // At how much resource you start with
    [SerializeField] float healthDecayMult = 1f; // Multiplier for how fast health decays while not in safe zone
    [SerializeField] float drainDelay = 5f; // Delay between resource drains to restore health
    [SerializeField] float hpThreshold = 25f; // At what health total we start draining resource for health
    [SerializeField] float resourceDrain = 5f; // How much resource is converted into health at once
    [SerializeField] float healthRestoreMult = 5f; // Multiplier for how much health we gain per second from a safe zone
    [SerializeField] float overheatMult = 2f; // Multiplier for how much health is drained because of Overheat. 2 = twice as much health drain etc.
    [SerializeField] float resourceBurnRate = 0.5f; // Multiplier for how much less/more your resource drains while using Overheat
    [SerializeField] float overheatRegenMult = 0.2f; // How much health we regen while Overheating using resource
    [SerializeField] bool decayHealthInOverheat = false; // Do we continue health decay when Overheat is active.
    [SerializeField] float setPickUpGain = 10f; // Set how much resource we gain for each resource pickup

    public static float pickUpGain; // How much resource we gain from a single pickup
    public static float playerHealth = 100f; // Current health
    public static float playerResource = 0f; // Current resource
    public static float maxResource = 100f; // Limit to how much resource we can have
    public static float maxHealth = 100f; // Limit to how much health we can have
    public static bool playerDead = false; // Helper flag for player death
    public static bool atSafeZone = false; // Helper flag for checking if we're at a safe zone
    public static bool overheatActive = false; // Helper flag to check if we're overheating
    bool debugShow = false; // Helper flag for flipping debug UI on and off

    // Variables for handling updraft
    private Rigidbody2D rb;
    public static bool updrafting = false;
    public float updraftVelocity = 100f;

    // Object references for UI and overheat hitbox
    public GameObject overheatHitbox;
    GameObject deathScreen;
    GameObject pauseScreen;
    public GameObject debugUI;
    public Text resourceUI;
    public Text healthUI;
    public Text alphaUI;
    public Text targetAlphaUI;
    private bool paused = false;

    // Misc. helper variables
    private float drainTimer = 0;

    // Variables for ResetPlayer
    GameObject player;
    public float deathScreenDelay = 0.5f;
    private float deathScreenTimer = 0f;
    public bool resetHealth = true;
    public bool resetResource = true;

    // Variables for handling light aura overlay changes
    public SpriteRenderer playerLight;
    Color playerLightColor;
    public float playerOHAlpha = 0.4f;
    public float overheatAlphaFadeMult = 2f;
    public float playerLightGradMin = 0.5f;
    public float playerLightGradMax = 0.8f;
    public float playerLightFadeSec = 0.1f;
    //float playerLightCoef;
    float targetLightAlpha;

    // Player Light Aura stuff
    public GameObject playerAura;
    //public GameObject playerAuraHigh;
    //public GameObject playerAuraLow;
    public SpriteRenderer playerAuraSprite;
    Color playerAuraColor;
    public float highHP = 50f;
    public float midHP = 20f;
    public float lowHP = 10f;
    public Color auraColorFull = new Color(0.77f, 0.65f, 0.30f, 0.6f);
    public Color auraColorMid = new Color(0.55f, 0.44f, 0.08f, 0.6f);
    public Color auraColorLow = new Color(0.57f, 0.01f, 0.01f, 0.6f);
    public Color auraColorOverheat = new Color(0f, 0f, 0f, 0.9f);
    private Color auraTargetColor;
    public float auraColorBlendMult = 1f;
    float playerAuraScale;

    //Animator stuff
    public Animator animator;


    void Awake()
    {
        // Initialize UI
        debugUI = GameObject.Find("DebugUI");
        debugUI.SetActive(false);
        deathScreen = GameObject.Find("DeathScreenUI");
        deathScreen.SetActive(false);
        pauseScreen = GameObject.Find("PauseMenuUI");
        pauseScreen.SetActive(false);
        Cursor.visible = false;

        // Initialize Aura references
        playerAura = GameObject.Find("Aura");
        //playerAuraHigh = GameObject.Find("AuraHigh");
        //playerAuraLow = GameObject.Find("AuraLow");

        // Setting Light overlay alpha to set value
        playerLightColor.a = playerLightGradMax;

        // Initialize player character reference
        player = GameObject.Find("PlayerCharacter");

        // Set Health and Resource pools to set values
        playerHealth = startingHealth;
        playerResource = startingResource;

        // Set how much resource / HP we gain from pickups
        pickUpGain = setPickUpGain;

        // Initialize object reference to Overheat hitbox
        overheatHitbox = GameObject.Find("OverheatHitbox");

        // Initialize object reference to players Rigidbody component
        rb = GetComponent<Rigidbody2D>();


        //resourceUI =  .Find("health");
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButton("Fire3")) // Here we're "listening" if the player activates Overheat. Press and keep <Shift> down to keep Overheat active
        {
            overheatActive = true;
        }

        else if (!Input.GetButton("Fire3")) // If <Shift> is released, Overheat is deactivated
        {
            overheatActive = false;
        }

        PauseHandler();
        
        OverheatFlipper(); // Enables & Disables Overheat hitbox depending on the value of overheatActive
        
        PlayerLight(); // Handles light overlay changes

        DebugUI(); // Handles toggling of Debug UI

        // Debug UI
        
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

        if (playerResource > maxResource) // If player resource goes over the maximum (def.100) for some reason, we set it to max.
        {
            playerResource = maxResource;
        }

        if (playerHealth > maxHealth) // If player health goes over the maximum (def. 100) for some reason, we set it to max.
        {
            playerHealth = maxHealth;
        }

        if (playerResource < 0) // If player resource goes below zero, we set it to zero.
        {
            playerResource = 0;
        }

        if (playerDead) // If player is flagged as dead, we run the reset command,
        {
            deathScreen.SetActive(true);
            //Time.timeScale = 0.25f;
            Debug.Log("You're dead dawg");

            if (deathScreenTimer >= deathScreenDelay)
            {
                //Time.timeScale = 1f;
                ResetPlayer();
                playerDead = false; // Flip players death flag back
                animator.SetBool("AlreadyDead", false);
                deathScreen.SetActive(false);
                deathScreenTimer = 0f;
            }

            deathScreenTimer += Time.deltaTime;
        }

        if (updrafting)
        {
            //rb.velocity = new Vector2(0f, updraftVelocity);
            rb.AddForce(new Vector2(0f, updraftVelocity));
        }

        AuraHandler();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RefuelZone")
        {
            atSafeZone = true;
            Debug.Log("Entered Safe Zone");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RefuelZone")
        {
            atSafeZone = false;
            Debug.Log("Left Safe Zone");
        }
    }

    public void ResetPlayer() // Method we can call to in any script to reset the player.
    {
        if (resetHealth) // Reset player health if it's enabled in engine
        {
            playerHealth = startingHealth;
        }

        if (resetResource) // Reset player resource to starting resource if enabled.
        {
            playerResource = startingResource;
        }
        
        player.transform.position = CheckpointHandler.lastCheckpoint;
    }

    public void OverheatFlipper()
    {
        if (overheatActive)
        {
            overheatHitbox.SetActive(true);
        }

        else if (!overheatActive)
        {
            overheatHitbox.SetActive(false);
        }
    }

    public void PlayerLight()
    {
        //targetLightAlpha = playerLightGradMax - (playerHealth / maxHealth * 0.3f);

        targetLightAlpha = playerLightGradMax - (playerHealth / maxHealth * (playerLightGradMax - playerLightGradMin));

        if (targetLightAlpha > 1) // Failsafes if alpha values go beyond the allowed range
        {
            targetLightAlpha = 1;
        }
        else if (targetLightAlpha < 0)
        {
            targetLightAlpha = 0;
        }

        if (overheatActive)
        {
            if (playerLightColor.a > playerOHAlpha)
            {
                playerLightColor.a -= Time.deltaTime * playerLightFadeSec * overheatAlphaFadeMult;
            }
        }

        else if (!overheatActive)
        {
            //playerLightColor.a = playerLightCoef;
            //targetLightAlpha = playerLightCoef;

            if (playerLightColor.a < targetLightAlpha) // playerLightGradMax / playerLightCoef
            {
                //playerLightColor.a += Time.deltaTime * playerLightFadeSec;
                playerLightColor.a += Time.deltaTime * playerLightFadeSec;
            }

            else if (playerLightColor.a > targetLightAlpha)
            {
                playerLightColor.a -= Time.deltaTime * playerLightFadeSec;
            }

            //playerLightColor.a = targetLightAlpha;
        }

        //playerLightCoef = playerLightGradMax - (playerHealth / maxHealth * 0.3f);
        //playerLightColor.a = playerLightCoef;
        playerLight.color = playerLightColor;
    }

    public void DebugUI()
    {
        resourceUI.text = "Resource: " + Mathf.RoundToInt(playerResource); // Updating UI for Resource value
        healthUI.text = "HP: " + Mathf.RoundToInt(playerHealth); // Updating UI for Health value
        alphaUI.text = "Light alpha: " + playerLightColor.a; //Mathf.RoundToInt(playerLightColor.a); Updating UI to show value of light overlay Alpha
        targetAlphaUI.text = "Target alpha: " + targetLightAlpha; //Mathf.RoundToInt(playerLightColor.a); Updating UI to show what the value of Alpha should be

        if (Input.GetButtonDown("Fire2"))
        {
            if (!debugShow)
            {
                debugShow = true;
                debugUI.SetActive(true);
                Cursor.visible = true;
            }

            else if (debugShow)
            {
                debugShow = false;
                debugUI.SetActive(false);
                Cursor.visible = false;
            }
        }
    }

    void AuraHandler()
    {
        //if (playerHealth >= 50)
        //{
        //    playerAuraHigh.SetActive(true);
        //    playerAura.SetActive(false);
        //    playerAuraLow.SetActive(false);
        //}

        //else if (playerHealth >= 10 && playerHealth < 50)
        //{
        //    playerAuraHigh.SetActive(false);
        //    playerAura.SetActive(true);
        //    playerAuraLow.SetActive(false);
        //}

        //else if (playerHealth < 10)
        //{
        //    playerAuraHigh.SetActive(false);
        //    playerAura.SetActive(false);
        //    playerAuraLow.SetActive(true);
        //}

        if (playerHealth >= highHP && !overheatActive)
        {
            auraTargetColor = auraColorFull;
        }

        else if (playerHealth >= midHP && playerHealth < highHP && !overheatActive)
        {
            auraTargetColor = auraColorMid;
        }

        else if (playerHealth < midHP)
        {
            auraTargetColor = auraColorLow;
        }

        else if (overheatActive && playerHealth > midHP)
        {
            auraTargetColor = auraColorOverheat;
        }

        playerAuraSprite.color = Color.Lerp(playerAuraSprite.color, auraTargetColor, Time.deltaTime * auraColorBlendMult);
        // playerAuraSprite.color = Color.Lerp(playerAuraSprite.color, playerAuraColor, 1f);
        // playerAuraSprite.color = playerAuraColor;
        // playerAuraColor placeholder
    }

    void PauseHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                pauseScreen.SetActive(true);
                Cursor.visible = true;
                Time.timeScale = 0f;
                // Play menu music
                // do other stuff while paused
            }

            else if (paused)
            {
                paused = false;
                pauseScreen.SetActive(false);
                Cursor.visible = false;
                Time.timeScale = 1f;
                // We return to the land of the living
            }

        }
    }
}
