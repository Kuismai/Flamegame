using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuddleKZ : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerGPMechanics.playerDead = true;
        }
    }
}
