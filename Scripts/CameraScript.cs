using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float yAxisValue = Input.GetAxisRaw("Vertical");
	
		transform.Translate(new Vector3(0.0f, yAxisValue, 0.0f));

	}
}
