using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createbrick : MonoBehaviour {

    public NewtonVR.NVRHand rtouch;

    public OVRInput.Controller controller;


    public GameObject contr;

	// Use this for initialization
	void Start () {
        contr = GetComponent<GameObject>();
        rtouch = GetComponent<NewtonVR.NVRHand>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        

        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("Lego_bricks/Brick_2x6", typeof(GameObject)));
            go.transform.position.Set(rtouch.CurrentPosition.x, rtouch.CurrentPosition.y, rtouch.CurrentPosition.z);
            go.AddComponent<NewtonVR.controllercube3>();
        }
		
	}
}
