using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScript : MonoBehaviour
{

    GameObject fire;

    private void Awake()
    {
        fire = GameObject.Find("Fire");
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
