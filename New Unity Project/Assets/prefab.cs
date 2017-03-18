using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefab : MonoBehaviour {

    public GameObject cubePrefab;
    GameObject cubePrefabClone;
    

	void Update () {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(1))
            {
                cubePrefabClone = Instantiate(cubePrefab, hit.point, Quaternion.identity) as GameObject;
            }

        }
    }
}
