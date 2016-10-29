using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	int currentG = 10;
	// Use this for initialization
	void Start () {
		//Rigidbody rb = GetComponent<Rigidbody>();
		//rb.
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Physics.gravity = new Vector3 (0, currentG, 0);
			currentG = currentG * -1;
		}
	}
}
