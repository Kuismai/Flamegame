using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropDamage : MonoBehaviour
{
    [SerializeField] float dropletDamage = 40f;
    public bool overheatIgnoresDroplets = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (overheatIgnoresDroplets && collision.gameObject.tag == "Player") // If the colliding object has the Player-tag
        {
            if (PlayerGPMechanics.overheatActive) // If overheat is active, don't take damage and evaporate the droplet
            {
                // Play fizzle sound & animation
                Destroy(gameObject);
            }


            else if (!PlayerGPMechanics.overheatActive || !overheatIgnoresDroplets) // If overheat is not active or overheat doesn't ignore droplets, take damage and delete the droplet
            {
                // Ouchie
                PlayerGPMechanics.playerHealth -= dropletDamage;
                Destroy(gameObject);
            }
        }

        else if (collision.gameObject.tag == "DropletExit")
        {
            Destroy(gameObject);
        }
    }
}
