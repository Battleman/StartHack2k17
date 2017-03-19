using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public OVRInput.Controller controller;

    Vector2 Joystick;
    GameObject go;
    Transform trans;
    Camera cam;
    OVRCameraRig test;
    

    // Use this for initialization
    void Start()
    {
        Joystick = new Vector2(0.0f, 0.0f);
        cam = GetComponent<Camera>();
        

    }

    // Update is called once per frame
    void Update()
    {

        Joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);

       // test.transform.Translate(Joystick.x* 5.0f, 0.0f, Joystick.y * 5.0f);

    }
}

