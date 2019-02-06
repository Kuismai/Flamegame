using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObject : MonoBehaviour {

    private static PersistentGameObject control;

    private void Awake()
    {
        //Allows only one of this object to exist in the game:
        if (control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
