using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBoardScript : MonoBehaviour {

	public GameObject DieBoard;
	
	bool DeathEndGame = false;
	bool AuxVar = false;
	float TimerBoard;
	
	// Use this for initialization
	void Start () {
		
		DieBoard.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//This deactivates the script SamplePlayerController once the bool variable SlaveDeath is true
		//Deactivating SamplePlayerController avoid any further movement from the controller, 
		//but user can still turn its head
		if(GeneralController.Instance.GameOver == true && DeathEndGame == false){
			DeathEndGame = true;
			TimerBoard = Time.time;
		}
		
		if(DeathEndGame == true && Time.time >= TimerBoard + 5.0f && AuxVar == false){ 
			DieBoard.SetActive(true);
			AuxVar = true;
		}
	}
}