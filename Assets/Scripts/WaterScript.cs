using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour {

	public GameObject Trash;
	
	bool WaterGuide;
	
	// Use this for initialization
	void Start () {
		
		WaterGuide = true;
		
	}
	
	// Update is called once per frame`
	void Update () {

	}
	
	void OnTriggerEnter(Collider other)
    {	
		
		
		//If the user approaches the water, the trash gets instantiated.
        if (other.tag == "Player"){
		
			Instantiate(Trash, new Vector3(32.40f, 16.8f, -21.72f), Quaternion.identity);
		
		}
		
    }
	
}
