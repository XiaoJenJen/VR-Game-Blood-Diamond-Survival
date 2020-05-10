using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeDiamond : MonoBehaviour {

	public GameObject FlareDiamond;
	AudioSource diamondSource; //An audio source variable
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		diamondSource = GetComponent<AudioSource>();
		
	}
	
	void OnTriggerEnter(Collider other)
    {	
		diamondSource.Play();
		
		//This script is used to identify each of the diamonds, since each one has different value
		//Diamond 1 = 50
		//Diamond 2 = 100
		//Diamond 3 = 200
		//It also instantiates a flare at the bucket location once the diamond is put inside it
        if (other.tag == "Diamond1"){
			GeneralController.Instance.WarlordDebt += -50;
			Instantiate(FlareDiamond, gameObject.transform.position, Quaternion.identity);
		}
		
		if (other.tag == "Diamond2"){
			GeneralController.Instance.WarlordDebt += -100;
			Instantiate(FlareDiamond, gameObject.transform.position, Quaternion.identity);
		}
		
		if (other.tag == "Diamond3"){
			GeneralController.Instance.WarlordDebt += -200;
			Instantiate(FlareDiamond, gameObject.transform.position, Quaternion.identity);
		}
		
		//Prints the Warlords Debt in the Console
		Debug.Log("WarlordDebt: " + GeneralController.Instance.WarlordDebt);
		
		//Afterwards, the diamond is destroyed to reduce computational operations
		Destroy(other);
		
    }
	
	
}
