using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickrotate : MonoBehaviour {

    public OVRInput.Controller controller;

    Vector2 Joystick;
    GameObject go;
    Transform trans;

	// Use this for initialization
	void Start () {
        Joystick = new Vector2(0.0f, 0.0f);
        go = GetComponent<GameObject>();
        trans = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {

        Joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);

        trans.Rotate(Joystick. x * 200.0f, 20.0f, Joystick.y * 150.0f);


		
	}
}
