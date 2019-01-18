using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public static bool isLit = true;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //if (stuff happens)
        //{
        //    We lit the bruh up
        //}
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLit)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerGPMechanics.updrafting = true;
            }
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
