using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class clickcontroller : MonoBehaviour {
	public Button myButton;

	/*void Start() {
		Button btn = myButton.getComponent<Button>();
		btn.onClick.AddListener (TaskOnClick);
	}*/

	void OnClick(){
		print("Mouse moved left");
	}
}
