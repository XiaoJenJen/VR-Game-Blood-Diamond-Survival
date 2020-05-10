using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAlarm : MonoBehaviour {

	AudioSource alarmSource; //An audio source variable
	
	bool stopAlarm;
	
	// Use this for initialization
	void Start () {
		
		stopAlarm = false;
		alarmSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GeneralController.Instance.gameTimer >= GeneralController.Instance.DeathTimer - 9.0f && !alarmSource.isPlaying && stopAlarm == false){ 
			alarmSource.Play();
			stopAlarm = true;
		}
		
		if(GeneralController.Instance.SlaveDeath == true) alarmSource.Stop();
		
	}
}
