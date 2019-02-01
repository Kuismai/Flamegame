using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public GameObject fire;
    //Transform child; 

    private void Awake()
    {
        //fire = GameObject.Find("Fire"); // 
        fire = gameObject.transform.Find("Fire").gameObject;
        //child = gameObject.transform.Find();
        fire.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OverheatHitbox")
        {
            fire.SetActive(true);
        }
    }
}
