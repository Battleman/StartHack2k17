using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllercube3 : MonoBehaviour {

	private bool blocked ;
	private Vector3 count;
	public float size1 = 1f ;

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



	void OnMouseDown(){
		isMoving = true;
	}

	void OnMouseUp(){
		isMoving = false;
	}

	public void OnMouseDrag() {
		if (!hasChild)
        {
			print("ahah");
            transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
           // transform.position = new Vector3(transform.position.x, transform.position.y, 20.0f);
        }
		else {
			count += new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0.0f);
			while (count.x >= size1) {
				//print ("++");
				count.x -= size1;
				transform.position +=  new Vector3 ( size1, 0.0f, 0.0f);
			}

			while (count.x <= -size1) {
				//print ("--");
				count.x += size1;
				transform.position -=  new Vector3 ( size1, 0.0f, 0.0f);
			}

			while (count.z >= size1) {
				count.z -= size1;
				transform.position +=  new Vector3 (  0.0f, 0.0f , size1);
			}

			while (count.z <= -size1) {
				count.z += size1;
				transform.position -= new Vector3 (  0.0f, 0.0f , size1);
			}

			if (count.y > 2*size1) {
				//print ("quit high");
				transform.position +=  new Vector3 ( 0.0f, 2*size1, 0.0f);
				hasChild = false;
			}


			if (count.y < -child.GetComponent<Collider> ().bounds.size.y -2*size1) {
				//print ("quit low");

				transform.position -=  new Vector3 ( 0.0f, child.GetComponent<Collider> ().bounds.size.y +2*size1, 0.0f);
				hasChild = false;
			}
			Vector3 pos = transform.position;
			Vector3 childPos = child.transform.position; 
			Vector3 currSize = GetComponent<Collider> ().bounds.size;
			Vector3 childSize = child.GetComponent<Collider> ().bounds.size;
			if (pos.x > childPos.x + currSize.x / 2 + childSize.x / 2) {
				print ("rate");
				print (count);
				print ("size");
				print (childSize);
				print (transform.position);
				hasChild = false;
			}

			if (pos.x < childPos.x - currSize.x / 2 - childSize.x / 2) {
				print ("bla");
				print (count);
				print ("size");
				print (childSize);
				print (transform.position);
				hasChild = false;
			}

			if (pos.z > childPos.z + currSize.z / 2 + childSize.z / 2) {
				print ("rate");
				hasChild = false;
			}

			if (pos.z < childPos.z - currSize.z / 2 - childSize.z / 2) {
				print ("bla");
				hasChild = false;
			}
		}
	

	}
		
		void OnTriggerEnter(Collider col) {
		if (isMoving && !hasChild) {
			print ("ahahahahahah");
			while (col.gameObject.transform.parent != null) {
				col = col.gameObject.transform.parent.GetComponent<Collider> ();
			}



			//TODO change for bricks of different size and z as x and from dowm

			child = col.gameObject;
			hasChild = true;


			blocked = true;
			count = new Vector3 (0.0f, 0.0f, 0.0f);

			Vector3 colPosition = col.gameObject.transform.position;
			Vector3 colSize = col.bounds.size;
			Vector3 colRot = col.gameObject.transform.rotation.eulerAngles;
			Vector3 currSize = GetComponent<Collider> ().bounds.size;
			Vector3 currPos = transform.position;


			transform.position = colPosition;
			//transform.rotation = col.gameObject.transform.rotation;

			/*transform.position += new Vector3 ((currSize.x - colSize.x) / 2, 0.0f, 0.0f);

			Vector3 addRot = Vector3.RotateTowards (,colRot, 4f, 1.0f);
			addRot.Normalize();
            print(addRot);*/
				if (currPos.y > colPosition.y) {
				transform.position += new Vector3 (0.0f, 1.2f, 0.0f);
			} else {
				transform.position -= new Vector3 (0.0f, 1.2f, 0.0f);
			}
			
			//transform.rotation = col.gameObject.transform.rotation;

			/*if (colPosition.x - colSize.x / 4 > currPos.x) {
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
				
