using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

    public int ropeLength; // number of rope pieces
    public GameObject ropePiece;
    public float spawnDistance;
    public GameObject firstConnectPoint, lastConnectPoint;
    public Vector2 firstConnectedAnchor, lastConnectedAnchor;

    private void Awake()
    {
        if (firstConnectPoint != null && lastConnectPoint != null)
        {
            CalculateDistance(firstConnectPoint, lastConnectPoint);
        }

        CreateNewRope(ropeLength, firstConnectPoint, lastConnectPoint);
    }

    void CreateNewRope(int length, GameObject firstConnect, GameObject lastConnect)
    {
        GameObject currentObj, previousObj = null;

        if (firstConnect != null)
        {
            // Move the rope to the first connect point:
            gameObject.transform.position = firstConnect.transform.position;
        }

        // Instantiate rope pieces:
        // TODO: rope piece rotation towards second connect point?
        for (int i = 0 ; i < length ; i++)
        {
            currentObj = Instantiate(ropePiece, 
                            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (spawnDistance * i), gameObject.transform.position.z),
                            new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w));

            if (i == 0 && firstConnect != null)
            {
                // Connect first rope piece to firstConnect using hingePoint2D:
                currentObj.GetComponent<HingeJoint2D>().connectedBody = firstConnect.GetComponent<Rigidbody2D>();
                currentObj.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
                currentObj.GetComponent<HingeJoint2D>().connectedAnchor = firstConnectedAnchor;
            }

            if (i > 0)
            {
                // Connect rope piece's hingePoint2D to the previous rope piece:
                currentObj.GetComponent<HingeJoint2D>().connectedBody = previousObj.GetComponent<Rigidbody2D>();
            }

            if (i == (length - 1) && lastConnect != null)
            {
                // Create a new hingeJoint2D and connect it to lastConnect:
                HingeJoint2D joint = currentObj.AddComponent<HingeJoint2D>();
                joint.anchor = new Vector2(0f, -0.75f);
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = lastConnectedAnchor;
                joint.connectedBody = lastConnect.GetComponent<Rigidbody2D>();
            }

            currentObj.transform.parent = gameObject.transform;
            previousObj = currentObj;
        }
    }

    void CalculateDistance(GameObject obj1, GameObject obj2)
    {
        // Calculates distance between two game objects:
        float distance = Vector2.Distance(obj1.transform.position, obj2.transform.position);
        Debug.Log("Distance between " + obj1.name + " and " + obj2.name + ": " + distance);

        // Set rope length based on distance:
        //ropeLength = Mathf.RoundToInt(distance * 2);
    }
}