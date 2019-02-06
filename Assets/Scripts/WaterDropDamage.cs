using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropDamage : MonoBehaviour
{
    [SerializeField] float dropletDamage = 40f;
    public bool overheatIgnoresDroplets = true;

    private AudioSource damageSound;
    private AudioSource waterDropLanding;
    private GameObject sounds;

    public void Start()
    {
        sounds = GameObject.Find("SFX");
        damageSound = sounds.transform.Find("damageSound").gameObject.GetComponent<AudioSource>();
        waterDropLanding = sounds.transform.Find("waterDropLanding").gameObject.GetComponent<AudioSource>();

        // waterDrop = sounds.GetComponentInChildren<AudioSource>();
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (overheatIgnoresDroplets && collision.gameObject.tag == "Player") // If the colliding object has the Player-tag
        {
            if (PlayerGPMechanics.overheatActive) // If overheat is active, don't take damage and evaporate the droplet
            {
                // Play fizzle sound & animation
                damageSound.Play();
                Destroy(gameObject);
            }


            else if (!PlayerGPMechanics.overheatActive || !overheatIgnoresDroplets) // If overheat is not active or overheat doesn't ignore droplets, take damage and delete the droplet
            {
                // Ouchie
                damageSound.Play();
                PlayerGPMechanics.playerHealth -= dropletDamage;
                Destroy(gameObject);
            }
        }

        else if (collision.gameObject.tag == "DropletExit")
        {
            waterDropLanding.Play();
            Destroy(gameObject);
        }
    }
}
