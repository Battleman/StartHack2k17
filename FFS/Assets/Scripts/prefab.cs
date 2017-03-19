using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefab : MonoBehaviour {


	[Header("Controller type")]
	public OVRInput.Controller handType;
	public enum ControllerHand { Left, Right };
    public GameObject cubePrefab;
    GameObject cubePrefabClone;
    

	void Update () {
		gameObject.transform.localPosition = OVRInput.GetLocalControllerPosition(handType);
		gameObject.transform.localRotation = OVRInput.GetLocalControllerRotation(handType);

		OVRInput.Update();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
			if (/*Input.GetMouseButtonDown(1)*/OVRInput.Get(OVRInput.Button.One, handType))
            {
				if (handType == OVRInput.Controller.RTouch) {
					print ("I detected a touch");
					float pos = Mathf.Max(0.0f, OVRInput.Get (OVRInput.RawAxis1D.RHandTrigger));
					cubePrefabClone = Instantiate (cubePrefab, hit.point + new Vector3 (pos, pos*1.1f, pos), Quaternion.Euler (0, 0, 0)) as GameObject;
				}
            }

        }
    }
}
