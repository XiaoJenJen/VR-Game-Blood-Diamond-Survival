using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralController : MonoBehaviour {

	public static GeneralController Instance;
	
	public int DifficultyGame, NumberBugs, NumberRocks, DiamondRockSmall, DiamondRockMed, DiamondRockLarge;
	public int WarlordDebt, MaxHungerBar, MaxThirstBar, HungryNumber, ThristNumber;
	public float DeathTimer;
	public bool SlaveDeath, GameOver, GameWin;
	public bool scoreLow, scoreMedium, scoreHigh;
	public bool startGameVar;
	
	public float restartTimer, gameTimer, startTime;
	bool AuxRestartTimer, AuxVar;


	// Use this for initialization
	void Start () {

	}
	
	void Awake(){
		
		//Makes an instance for easy access of variables through other scripts
        //startTime = Time.time;
		
		Instance = this;
		
		scoreLow = false;
		scoreMedium = false;
		scoreHigh = false;
		
		GameOver = false;
		GameWin = false;
		
		restartTimer = 0.0f;
		AuxRestartTimer = false;
		
		startGameVar = false;
		AuxVar = false;
		
		//Difficulty level of the game
		//1: Easy
		//2: Medium
		//3: Hard
		DifficultyGame = 0;
		
		//Number or rocks available
		NumberRocks = 20;
		
		//Maximum level for HungerBar and ThirstBar
		MaxHungerBar = 15;
		HungryNumber = 15;
		
		MaxThirstBar = 20;
		ThristNumber = 20;
		
		WarlordDebt = 500;
		
		//DeathTimer adjusted to 5 mintutes
		DeathTimer = 300.0f;
		SlaveDeath = false;
		
    }
	
	// Update is called once per frame
	void Update () {
		
		//If difficulty is increased, the probability of diamonds drop per rock is decreased, as well as the number of bugs to eat
		if(DifficultyGame == 1 && AuxVar == false){
			DiamondRockSmall = 2;	
			DiamondRockMed = 3;
			DiamondRockLarge = 4;
			NumberBugs = 8;
			WarlordDebt = 500;
			AuxVar = true;
			startGameVar = true;
			
			//Makes an instance for easy access of variables through other scripts
			startTime = Time.time;
			
		}
		
		if(DifficultyGame == 2 && AuxVar == false){
			DiamondRockSmall = 2;	
			DiamondRockMed = 2;
			DiamondRockLarge = 3;
			NumberBugs = 4;
			WarlordDebt = 750;
			AuxVar = true;
			startGameVar = true;
			
			//Makes an instance for easy access of variables through other scripts
			startTime = Time.time;
		}
		
		if(DifficultyGame == 3 && AuxVar == false){
			DiamondRockSmall = 2;	
			DiamondRockMed = 2;
			DiamondRockLarge = 2;
			NumberBugs = 2;
			WarlordDebt = 1000;
			AuxVar = true;
			startGameVar = true;
			
			//Makes an instance for easy access of variables through other scripts
			startTime = Time.time;
		}
		
		
		if(startGameVar == true){
		
			//timer update
			gameTimer = Time.time - startTime;
			
			//If any of the conditions is met, the bool variable SlaveDeath changes to true
			if(gameTimer >= DeathTimer + 1.5f || HungryNumber == 0 || ThristNumber == 0){
				SlaveDeath = true;		
				GameOver = true;
			}
			
			if(WarlordDebt <= 0 &&  GameWin == false){
			
				GameWin = true;
				if(gameTimer < 120.0f) scoreHigh = true;
				if(gameTimer >= 120.0f && gameTimer < 240.0f) scoreMedium = true;
				if(gameTimer >= 240.0f && gameTimer < 300.0f) scoreLow = true;
				
			}		
			
			//Used to restart game
			if(GameOver == true || GameWin == true){
				
				if(AuxRestartTimer == false){
					restartTimer = Time.time;
					AuxRestartTimer = true;
				}
				
				if(Input.anyKey && gameTimer > restartTimer + 10) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
		
	}
}
