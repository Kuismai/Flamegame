using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private static bool isLit = false;
    private GameObject updraftBox;
    //private GameObject smokeFX;

    void Start()
    {
        updraftBox = GameObject.Find("UpdraftBox");
        //smokeFX = GameObject.Find("SmokeParticles");
    }

    private void FixedUpdate()
    {
        if (isLit)
        {
            updraftBox.SetActive(true);
        }

        else if (!isLit)
        {
            updraftBox.SetActive(false);
        }

        //Debug.Log("" + isLit);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isLit)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerGPMechanics.updrafting = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OverheatHitbox" || collision.name == "BurnHitbox")
        {
            //isLit = true;
            if (!isLit)
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                isLit = true;
            }
            //gameObject.GetComponentInChildren<ParticleSystem>().Play();
            //smokeFX.GetComponent<ParticleSystem>().Play();
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isLit)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerGPMechanics.updrafting = false;
            }
        }
    }
}
