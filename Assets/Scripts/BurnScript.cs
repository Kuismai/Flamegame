using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour
{
    public float burnTimerMax; // starting value for burn timer
    public float onFireThreshold; // if burnTimer is under this value, the object starts to burn by itself (is on fire)
    public float burnTimer; // current burn value
    public bool isOnFire; // is the object on on fire or not

    // Use this for initialization
    void Start()
    {
        burnTimer = burnTimerMax;
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
        if (other.name == "WaterPuddle" && isOnFire)
        {
            ExtinquishObject();
        }
        else if (other.name == "OverheatHitbox" || other.name == "BurnHitbox")
        {
            BurnObject(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "WaterPuddle" && isOnFire)
        {
            ExtinquishObject();
        }
        else if (other.name == "OverheatHitbox" || other.name == "BurnHitbox")
        {
            BurnObject(other);
        }
    }

    private void BurnObject(Collider2D collider)
    {
        burnTimer -= Time.deltaTime;

        if (burnTimer <= onFireThreshold && !isOnFire)
        {
            isOnFire = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;

            // For smoke effect:
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    private void ExtinquishObject()
    {
        isOnFire = false;
        burnTimer = burnTimerMax;
        gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;

        // For smoke effect:
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
