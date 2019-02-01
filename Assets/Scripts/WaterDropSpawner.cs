using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropSpawner : MonoBehaviour
{
    public GameObject droplet;
    private AudioSource waterDrop;
    private GameObject sounds;

    public void Start()
    {    
        sounds = GameObject.Find("SFX");
        waterDrop = sounds.transform.Find("waterDrop").gameObject.GetComponent<AudioSource>();
        // waterDrop = sounds.GetComponentInChildren<AudioSource>();
    } 
    
    [SerializeField] float spawnDelay = 2f;
    private float spawnTimer = 0;
    

    private void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnDelay)
        {
            Instantiate(droplet, transform.position, Quaternion.Euler(0, 0, 0));
            spawnTimer = 0;
            waterDrop.Play();
        }
    }
}
