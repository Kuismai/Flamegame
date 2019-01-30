using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    private Vector2 spawnPoint;

	// Use this for initialization
	void Start ()
    {
        spawnPoint = gameObject.GetComponentInChildren<Transform>().transform.position;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered " + gameObject.name);

        if (other.gameObject.tag == "Player")
        {
            CheckpointHandler.lastCheckpoint = spawnPoint;
            //TODO: enter safezone?
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        //TODO: exit safezone?
    //    }
    //}
}
