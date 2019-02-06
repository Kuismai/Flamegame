using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    public static bool levelComplete = false;
    public int levelToLoad;
    private float endTimer;
    public float endDelay = 2f;

    private void Awake()
    {
        endTimer = 0f;
    }

    private void Update()
    {
        if (levelComplete)
        {
            endTimer += Time.deltaTime;
        }

        if (levelComplete && endTimer >= endDelay)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // If the object colliding with resource pickup has the Player-tag
        {
            levelComplete = true;
        }
    }

}
