using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropSpawner : MonoBehaviour
{
    public GameObject droplet;
    [SerializeField] float spawnDelay = 2f;
    private float spawnTimer = 0;

    private void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnDelay)
        {
            Instantiate(droplet, transform.position, Quaternion.Euler(0, 0, 0));
            spawnTimer = 0;
        }
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
