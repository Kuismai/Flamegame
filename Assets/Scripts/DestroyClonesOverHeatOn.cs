using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClonesOverHeatOn : MonoBehaviour {

	void Update () {
        if (gameObject.name != "overHeatOn")
            Destroy(this.gameObject, 0.5f);
    }
}
