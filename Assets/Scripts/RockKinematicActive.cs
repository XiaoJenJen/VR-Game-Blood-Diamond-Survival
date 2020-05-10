using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockKinematicActive : MonoBehaviour {

	bool RockKinematic;

	// Use this for initialization
	void Start () {
		RockKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		//There was an issue with the grabbing and the rigidbody interaction. The gravity was overwritten
		//when hitting an object. Therefore, this part activates Kinematic to the rocks after 15 seconds.
		//The rocks are instantiated above ground level and the 15 seconds are for them to settle on the ground.
		if(GeneralController.Instance.gameTimer > 15 && RockKinematic == false){
			GetComponent<Rigidbody>().isKinematic = true;
			RockKinematic = true;
		}
		
	}
}
