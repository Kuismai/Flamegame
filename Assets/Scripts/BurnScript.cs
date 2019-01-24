using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour {

    public float burnTimerMax; // starting value for burn timer
    public float onFireThreshold; // if burnTimer is under this value, the object starts to burn by itself (is on fire)
    public float burnTimer; // current burn value
    public bool isOnFire; // is the object on on fire or not


	// Use this for initialization
	void Start () {
        burnTimer = burnTimerMax;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isOnFire)
        {
            burnTimer -= Time.deltaTime;

            if (burnTimer <= 0)
            {
                // TODO: destroy object:
                Object.Destroy(gameObject);
            }
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "OverheatHitbox" || collision.name == "BurnHitbox")
        {
            burnTimer -= Time.deltaTime;

            if (burnTimer <= onFireThreshold && !isOnFire)
            {
                // TODO: activate burn hitbox for this object
                isOnFire = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
            }
        }
    }
}
