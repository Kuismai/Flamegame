using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRope : MonoBehaviour {

    public int ropeLength; // number of rope pieces
    public GameObject ropePiece;
    public float spawnDistance;
    public float rotationZ;
    public GameObject firstConnectPoint, lastConnectPoint;

    //// use this for initialization
    //void Start()
    //{

    //}

    //// update is called once per frame
    //void Update()
    //{

    //}

    private void Awake()
    {
        CreateNewRope(ropeLength);
    }

    void CreateNewRope(int length)
    {
        GameObject currentObj, previousObj = null;

        for (int i = 0 ; i < length ; i++)
        {
            currentObj = Instantiate(ropePiece, 
                            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (spawnDistance * i), gameObject.transform.position.z),
                            new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w));

            if (i == length && lastConnectPoint != null)
            {
                // TODO: connect last rope piece to a thing:

            }
            if (i > 0)
            {
                // connect first hingePoint2D to the previous rope piece:
                currentObj.GetComponent<HingeJoint2D>().connectedBody = previousObj.GetComponent<Rigidbody2D>();
            }

            if (i == length && lastConnectPoint != null)
            {
                // TODO: connect last rope piece to a thing using second hingePoint2D:
                // ?? :D ?
            }

            currentObj.transform.parent = gameObject.transform;
            previousObj = currentObj;
        }
    }
}