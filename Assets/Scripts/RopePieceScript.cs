using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePieceScript : MonoBehaviour
{
    public float burnTimerMax; // starting value for burn timer
    public float onFireThreshold; // if burnTimer is under this value, the object starts to burn by itself (is on fire)
    public float burnTimer; // current burn value
    public bool isOnFire; // is the object on on fire or not
    GameObject flameParticles;
    GameObject smokeParticles;

    // Use this for initialization
    void Start()
    {
        burnTimer = burnTimerMax;
        flameParticles = transform.Find("FlameParticles").gameObject;
        smokeParticles = transform.Find("SmokeParticles").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOnFire)
        {
            burnTimer -= Time.deltaTime;

            if (burnTimer <= 0)
            {
                Object.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water" && isOnFire)
        {
            Extinquish();
        }
    }

    public void Burn()
    {
        burnTimer -= Time.deltaTime;

        if (burnTimer <= onFireThreshold && !isOnFire)
        {
            isOnFire = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;

            // For flame effect:
            flameParticles.GetComponent<ParticleSystem>().Play();
            // For smoke effect:
            //smokeParticles.GetComponent<ParticleSystem>().Play();
        }
    }

    public void Extinquish()
    {
        isOnFire = false;
        burnTimer = burnTimerMax;
        gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;

        // For flame effect:
        flameParticles.GetComponent<ParticleSystem>().Stop();
        // For smoke effect:
        //smokeParticles.GetComponent<ParticleSystem>().Stop();
    }
}
