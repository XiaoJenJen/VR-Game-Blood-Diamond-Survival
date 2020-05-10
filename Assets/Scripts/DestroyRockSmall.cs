using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRockSmall : MonoBehaviour {

	public GameObject stoneCrash, Diamond1;
	
	AudioSource hitRockAudio; //An audio source variable for hitting rock
	
	int RockHits;
	int DiamondNumber, DiamondType;
	
	// Use this for initialization
	void Start () {
		
		RockHits = 0;
		DiamondNumber = Random.Range(1, GeneralController.Instance.DiamondRockSmall);
		
		hitRockAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
    {	
		//Hit counter for the rock
        if (other.tag == "hand"){
			
			RockHits += 1;
			
			//Include Hitting Sound
			hitRockAudio.Play();
			
			//Every hit reduces the HungerBar as well as the ThirstBar
			GeneralController.Instance.HungryNumber += -1;
			GeneralController.Instance.ThristNumber += -1;
			
			//Instantiates smalled rocks as to give the illusion of the bigger one breaking down
			//Smaller rocks require 2 hits to break
			if(RockHits == 2){
				Instantiate(stoneCrash, gameObject.transform.position, Quaternion.identity);
				Instantiate(stoneCrash, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
				
				//For Loop to randomly assign a type of diamond
				for(int i = 0; i < DiamondNumber; i++){
					DiamondType = Random.Range(1, 2);
					if(DiamondType == 1){
						Instantiate(Diamond1, gameObject.transform.position, Quaternion.identity);
					}
				}
			}
		}
		
		//hitRockAudio.Stop();
		
    }
	
	
}
