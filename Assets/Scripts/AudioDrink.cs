using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDrink : MonoBehaviour {

	AudioSource drinkSource; //An audio source variable

	int DrinkCopy;
	
	// Use this for initialization
	void Start () {
		
		drinkSource = GetComponent<AudioSource>();
		DrinkCopy = GeneralController.Instance.ThristNumber;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GeneralController.Instance.ThristNumber > DrinkCopy && !drinkSource.isPlaying) drinkSource.Play();
		
		if(GeneralController.Instance.ThristNumber != DrinkCopy){ 
			DrinkCopy = GeneralController.Instance.ThristNumber;
		}
		
		//audio stops if game ends
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && drinkSource.isPlaying) drinkSource.Stop();
		
	}
}
