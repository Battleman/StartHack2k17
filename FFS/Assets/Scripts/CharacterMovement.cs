using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    Vector2 LeftJoystick;

	// Use this for initialization
	void Start () {

        LeftJoystick = new Vector2(0.0f, 0.0f);
		
	}
	
	// Update is called once per frame
	void Update () {

        LeftJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

    }
}
