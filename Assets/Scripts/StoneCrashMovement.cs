using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCrashMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		//Used for when rumble rocks are instantiated
		transform.Translate(0.00f, 2.0f * Time.deltaTime, 0.0f);
		Destroy(gameObject, 8.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
