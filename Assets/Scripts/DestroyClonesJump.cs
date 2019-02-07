using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClonesJump : MonoBehaviour {
    
	void Update () {
        if (gameObject.name != "jumpSound")
            Destroy(this.gameObject, 1.0f);
    }
}
