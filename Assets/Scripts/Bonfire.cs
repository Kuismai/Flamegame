using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private static bool isLit = false;
    private GameObject updraftBox;

    void Start()
    {
        updraftBox = GameObject.Find("UpdraftBox");
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

        //else if (!isLit)
        //{
        //    updraftBox.SetActive(false);
        //}
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
        if (collision.gameObject.tag == "OverheatHitbox")
        {
            isLit = true;
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
