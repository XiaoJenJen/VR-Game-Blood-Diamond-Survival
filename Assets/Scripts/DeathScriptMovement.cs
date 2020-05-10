using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptMovement : MonoBehaviour {

	bool DeathEndGame = false;
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//This deactivates the script SamplePlayerController once the bool variable SlaveDeath is true
		//Deactivating SamplePlayerController avoid any further movement from the controller, 
		//but user can still turn its head
		if( (GeneralController.Instance.SlaveDeath == true || GeneralController.Instance.GameWin == true) && DeathEndGame == false){
			DeathEndGame = true;
			GetComponent<SamplePlayerController>().enabled = false;
		}
		
	}
}