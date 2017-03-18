using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllercube : MonoBehaviour {
	private bool blocked ;
	private Vector3 count;
	public float size1 = 1 ;

	private bool isMoving ;

	//child down parent up
	public GameObject child ;
	public bool hasChild ;

	public GameObject parent ;
	public bool hasParent ;

	//TODO : add parent

	//public bool clicked = false;
	// Use this for initialization
	void Start () {
		blocked = false;
		hasChild = false;
		hasParent = false;
		isMoving = false;
	}

	// Update is called once per frame
	/*void Update () {
		if (true) {
			transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") ,0.0f, Input.GetAxis("Mouse Y"));
		}
		// Création d'un nouveau vecteur de déplacement
			
	}*/

	void OnMouseDown(){
		isMoving = true;
	}

	void OnMouseUp(){
		isMoving = false;
	}

	void OnMouseDrag() {
		if (!hasChild)
			transform.position = transform.position + new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0.0f);
		else {
			count += new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0.0f);
			while (count.x >= size1) {
				count.x -= size1;
				transform.position +=  new Vector3 ( 1.0f, 0.0f, 0.0f);
			}

			while (count.x <= -1) {
				count.x += 1f;
				transform.position -=  new Vector3 ( 1.0f, 0.0f, 0.0f);
			}
			if (count.y > 2) {
				transform.position +=  new Vector3 ( 0.0f, 2.0f, 0.0f);
				blocked = false;
			}
			if (count.y < -child.GetComponent<Collider> ().bounds.size.y -2) {
				transform.position -=  new Vector3 ( 0.0f, 3.0f, 0.0f);
				blocked = false;
			}
			Vector3 pos = transform.position;
			Vector3 childPos = child.transform.position; 
			Vector3 currSize = GetComponent<Collider> ().bounds.size;
			Vector3 childSize = child.GetComponent<Collider> ().bounds.size;
			if (pos.x > childPos.x + childSize.x / 2 + childSize.x / 2) {
				hasChild = false;
			}

			if (pos.x < childPos.x - childSize.x / 2 - childSize.x / 2) {
				hasChild = false;
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (isMoving) {



			//TODO change for bricks of different size and z as x and from dowm

			child = col.gameObject;
			hasChild = true;


			blocked = true;
			count = new Vector3 (0.0f, 0.0f, 0.0f);

			print ("Mouse moved left");
			print (col.gameObject.transform.position);
			Vector3 colPosition = col.gameObject.transform.position;
			Vector3 colSize = col.bounds.size;
			Vector3 currSize = GetComponent<Collider> ().bounds.size;
			Vector3 currPos = transform.position;
			print (transform.position);


			transform.position = colPosition ;
			transform.rotation = col.gameObject.transform.rotation;

			transform.position += new Vector3 ((currSize.x - colSize.x) / 2, colSize.y, 0.0f);
			/*
			if (colPosition.x - colSize.x / 4 > currPos.x) {
				print ("yes");
				print (colSize.x / 4);
				print (currPos.x);

				transform.position += new Vector3 (-currSize.x + 1, 0.0f, 0.0f);
			} else if (colPosition.x - colSize.x / 4 < transform.position.x) {
				transform.position += new Vector3 (colSize.x - 1, 0.0f, 0.0f);
				print ("no");

			}*/

		}
	}
}

