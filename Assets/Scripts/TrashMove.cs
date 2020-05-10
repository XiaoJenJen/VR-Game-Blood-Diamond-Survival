using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMove : MonoBehaviour {

	AudioSource bubblesSource; //An audio source variable for bubbles

	float UpperBound, LowerBound, trashSpeed, TrashTimer;
	bool RemoveTrash;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 10);
		trashSpeed = 1.2f;
		TrashTimer = 0.0f;
		RemoveTrash = false;
		
		bubblesSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Plays bubble sounds
		if (!bubblesSource.isPlaying) bubblesSource.Play();
		//Stops bubbles sound
		if(Time.time - TrashTimer >= 5.0f) bubblesSource.Stop();
		
		//This part allows the trash to move upon the water, and go down again after 3.0 in time		
		if(transform.position.y <= 18.1f){
			transform.Translate(0.0f, trashSpeed * Time.deltaTime, 0.0f);
			TrashTimer = Time.time;
			RemoveTrash = true;
		}
		
		//This is the part to move the trash downward
		if(Time.time - TrashTimer >= 3.0f && RemoveTrash == true){
			trashSpeed = -1.2f;
			transform.Translate(0.0f, trashSpeed * Time.deltaTime, 0.0f);
		}	
	
	}
}
