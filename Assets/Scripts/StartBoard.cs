using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoard : MonoBehaviour
{
    public GameObject startBoard;
    private bool startBoardActive;

	public float stratBoardTimer;
	
	bool AuxVar;
	
    void Start()
    {
		AuxVar = false;
        startBoardActive = true;
		startBoard.SetActive(false);
    }

    void Update()
    {
		//Used to activate or deactivate the start board (instruction board) when required 
        if (GeneralController.Instance.startGameVar == true && startBoardActive == true){
			startBoard.SetActive(true);
			
			stratBoardTimer = Time.time;
			startBoardActive = false;
			AuxVar = true;
		}
		
		//After three seconds of the activation of the start board, any key input will disable it
		if (Time.time >= stratBoardTimer + 3.0f && Input.anyKey && GeneralController.Instance.startGameVar == true && AuxVar == true) {
			
			startBoard.SetActive(false);
			AuxVar = false;
			
		}
        
    }

}