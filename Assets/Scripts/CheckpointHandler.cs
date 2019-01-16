using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour {

    private GameObject player;
    public static Vector2 lastCheckpoint;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerCharacter");
        lastCheckpoint = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        //pressing R will return the player to last checkpoint:
        if (Input.GetKeyDown(KeyCode.R))
        {
            //restartFromLastCheckpoint();
            PlayerGPMechanics.ResetPlayer();
        }
	}

    //void restartFromLastCheckpoint()
    //{
    //    player.transform.position = CheckpointHandler.lastCheckpoint;
    //}
}
