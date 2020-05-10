using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEat : MonoBehaviour {

	AudioSource eatSource; //An audio source variable

	int HungryCopy;
	
	// Use this for initialization
	void Start () {
		
		eatSource = GetComponent<AudioSource>();
		HungryCopy = GeneralController.Instance.HungryNumber;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GeneralController.Instance.HungryNumber > HungryCopy && !eatSource.isPlaying) eatSource.Play();
		
		if(GeneralController.Instance.HungryNumber != HungryCopy){ 
			HungryCopy = GeneralController.Instance.HungryNumber;
		}
		
		//audio stops if game ends
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && eatSource.isPlaying) eatSource.Stop();
		
	}
}
