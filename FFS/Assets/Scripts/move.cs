using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        
    }

    void OnMouseDown()
    {
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void OnMouseDrag()
    {
        Vector3 mousePostition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePostition);
        rb.position = objPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }

    void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
}
