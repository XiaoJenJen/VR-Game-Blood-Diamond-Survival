using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBoardScript : MonoBehaviour {

	public GameObject WinBoard;
	public GameObject lowScoreBoard;
	public GameObject mediumScoreBoard;
	public GameObject HighScoreBoard;
	
	bool WinGame = false;
	
	// Use this for initialization
	void Start () {
		
		WinBoard.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//This deactivates the script SamplePlayerController once the bool variable SlaveDeath is true
		//Deactivating SamplePlayerController avoid any further movement from the controller, 
		//but user can still turn its head
		if(GeneralController.Instance.GameWin == true && WinGame == false){
			WinGame = true;
			WinBoard.SetActive(true);
			
			if(GeneralController.Instance.scoreLow == true) lowScoreBoard.SetActive(true);
			if(GeneralController.Instance.scoreMedium == true) mediumScoreBoard.SetActive(true);
			if(GeneralController.Instance.scoreHigh == true) HighScoreBoard.SetActive(true);
			
		}
		
	}
}