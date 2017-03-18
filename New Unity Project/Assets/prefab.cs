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
                print(cubePrefab);
                cubePrefabClone = Instantiate(cubePrefab, hit.point + new Vector3(0,0.5f,0), Quaternion.identity) as GameObject;
            }

        }
    }
}
