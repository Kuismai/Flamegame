using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClones : MonoBehaviour {

	void Update () { 
            if (gameObject.name != "fireFlySound")
                Destroy(this.gameObject, 0.0f);
    }
}
