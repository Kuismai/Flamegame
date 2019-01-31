using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public bool isLit = false;
    private GameObject updraftBox;
    private SpriteRenderer[] flames;

    void Start()
    {
        updraftBox = gameObject.transform.Find("UpdraftBox").gameObject;

        // Flames:
        flames = new SpriteRenderer[updraftBox.transform.childCount];
        for (int i = 0; i < flames.Length; i++)
        {
            flames[i] = updraftBox.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        }
    }

    // When the bonfire sets on fire:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OverheatHitbox" || collision.name == "BurnHitbox")
        {
            if (!isLit)
            {
                isLit = true;
                updraftBox.GetComponent<BoxCollider2D>().enabled = true;

                // Activate smoke particles: 
                gameObject.GetComponentInChildren<ParticleSystem>().Play();

                // Activate flames:
                for (int i = 0; i < flames.Length; i++)
                {
                    flames[i].enabled = true;
                }
            }
        }
    }
}