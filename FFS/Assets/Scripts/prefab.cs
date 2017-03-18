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
                cubePrefabClone = Instantiate(cubePrefab, hit.point+new Vector3(0,1,0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }

        }
    }
}
