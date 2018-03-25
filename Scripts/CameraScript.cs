using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	void Update () {
		float yAxisValue = Input.GetAxisRaw("Vertical");
		transform.Translate(new Vector3(0.0f, yAxisValue, 0.0f));
	}

}
