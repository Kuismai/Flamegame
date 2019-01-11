using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyZigZag : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(0f, 1.1f, 0);
    public float speed = 0.08f;
    public float amplitude = 0.5f;
    float tx = 0f;
    float ty = 0f;
    //resets position of the firefly
    void Awake()
    {
        transform.position = pos1;
    }
    // moves object from pos1 to pos 2
    void Update()
    {
        pos1.x = amplitude * Mathf.Cos(tx);
        pos1.y = speed * Mathf.Cos(ty);
        tx += Time.deltaTime;
        ty += Time.deltaTime * 6;
        transform.position = pos1;
    }

}
