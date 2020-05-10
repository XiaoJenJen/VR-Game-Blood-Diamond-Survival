using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillShot : MonoBehaviour {

	public GameObject ShootEffect;
	
	//Public material used for coroutine in blood screen
	public Material BloodScreen;
	
	//Independent bool variables for each shot, as to allow them with an interval of 0.5 in time
	bool Shoot1 = true;
	bool Shoot2 = false;
	bool Shoot3 = false;
	bool Shoot4 = false;
	
	bool ChangeColorBlack, ChangeColorRed;
	float shootTimer;
	float f, g;
	
	
	// Use this for initialization
	void Start () {
		
		//restarts the shooting timer
		shootTimer = 0.0f;
		
		//Restarts the blood screen to transparent
		Color bloodColor = BloodScreen.color;
		bloodColor.a = 0.0f;
		bloodColor.r = 1.0f;
		BloodScreen.color = bloodColor;
		
		ChangeColorBlack = false;
		ChangeColorRed = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Used to call coroutine BloodFade
		if(GeneralController.Instance.SlaveDeath == true && ChangeColorRed == true){
			StartCoroutine(BloodFade());
		}
		
		//Used to call coroutine BlackFade
		if(ChangeColorBlack == true){
			StartCoroutine(BlackFade());
		}
		
		//The following IF statement are for each independent shot.
		//If the timer is up, the slave will receive 4 shots, and the screen will turn
		//blood red after the fourth one, and then pitch black
		if(GeneralController.Instance.gameTimer >= GeneralController.Instance.DeathTimer && Shoot1 == true){
			shootTimer = Time.time;
			Instantiate(ShootEffect,transform.position,Quaternion.identity);
			Shoot1 = false;
			Shoot2 = true;
		}
		
		if(GeneralController.Instance.gameTimer - shootTimer >= 0.5f && Shoot2 == true){
			Instantiate(ShootEffect,transform.position,Quaternion.identity);
			Shoot2 = false;
			Shoot3 = true;
		}
		
		if(GeneralController.Instance.gameTimer - shootTimer >= 1.0f && Shoot3 == true){
			Instantiate(ShootEffect,transform.position,Quaternion.identity);
			Shoot3 = false;
			Shoot4 = true;
		}
		
		if(GeneralController.Instance.gameTimer - shootTimer >= 1.5f && Shoot4 == true){
			Instantiate(ShootEffect,transform.position,Quaternion.identity);
			Shoot4 = false;
			
		}
	
	}
	
	//Coroutine declaration BloodFade
	IEnumerator BloodFade(){ 
		
		//Uses alpha screen's color to change between its transparency
		for(f = 0; f <= 1.1f; f += 0.1f){
			Color bloodColor = BloodScreen.color;
			bloodColor.a = f;
			BloodScreen.color = bloodColor;
			
			//waits for the specified time to continue between frames
			yield return new WaitForSeconds(0.25f);
		}
		
		
		if(f >= 1.1f){
			ChangeColorBlack = true;
			ChangeColorRed = false;
		}
	}
	
	//Coroutine declaration for BlackFade
	IEnumerator BlackFade(){ 
		
		//Uses r screen's color to change between its color into black
		for(g = 1; g >= -0.1f; g += -0.1f){
			Color bloodColor = BloodScreen.color;
			bloodColor.r = g;
			BloodScreen.color = bloodColor;
			
			//waits for the specified time to continue between frames
			yield return new WaitForSeconds(0.25f);
		}
		
		if(g <= -0.1f){
			ChangeColorBlack = false;
		}

	}
	
	
}
