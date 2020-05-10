using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMinion1 : MonoBehaviour {

	AudioSource minionSource; //An audio source variable

	bool MinionGuide;
	
	// Use this for initialization
	void Start () {
		
		MinionGuide = false;
		minionSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GeneralController.Instance.gameTimer >= 60.0f && GeneralController.Instance.gameTimer <= 110.0f && MinionGuide == false){
			GetComponent<Collider>().enabled = true;
		}
		
	}
	
	
	void OnTriggerEnter(Collider other)
    {		
		
		
		
		if(GeneralController.Instance.gameTimer >= 60.0f && GeneralController.Instance.gameTimer <= 110.0f && MinionGuide == false && other.tag == "Player"){
			minionSource.Play();
			MinionGuide = true;
			GetComponent<Collider>().enabled = false;
		}
		
		//audio stops if game ends
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && minionSource.isPlaying) minionSource.Stop();
		
    }
	
}
