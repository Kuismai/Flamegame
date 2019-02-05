using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour
{
    Collider2D burnCollider;

    // Use this for initialization
    void Start()
    {
        burnCollider = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Rope")
        {
            other.gameObject.GetComponent<RopePieceScript>().Burn();
        }
    }
}
