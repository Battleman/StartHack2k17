using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll : MonoBehaviour {

    GameObject ball;

    float speed = 0.0f;

	// Use this for initialization
	void Start () {

        ball = gameObject;


		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            speed += 0.1f;
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed -= 0.1f;
        }
        else
        {
            
            speed -= 0.02f * Mathf.Sign(speed);
            if (speed <= 0.005f && speed >= -0.005f)
            {
                speed = 0.0f;
            }
            
        }

        ball.transform.Translate(0, 0, speed);
    }
}
