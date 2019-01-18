using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyZigZag : MonoBehaviour
{
    public GameObject followTarget;
    public Transform target;
    private Vector3 pos1;
    public float follow = 5f;
    public float speed = 0.08f;
    public float amplitude = 0.5f;
    float tx = 0f;
    float ty = 0f;

    void Start()
    {
        target = followTarget.GetComponent<Transform>(); // .FindGameObjectWithTag(followTarget)
    }

    //resets position of the firefly
    /*
     void Awake()
    
    {
        transform.position = pos1;
    }
    */

    // moves object from pos1 to pos 2
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, follow * Time.deltaTime);
        pos1.x = amplitude * Mathf.Cos(tx);
        pos1.y = speed * Mathf.Cos(ty);
        tx += Time.deltaTime;
        ty += Time.deltaTime * 6;
    }

}
