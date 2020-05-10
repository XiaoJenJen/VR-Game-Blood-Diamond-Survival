using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAssetsAppear : MonoBehaviour {

	//public GameObjects 
    public GameObject stoneSmall, stoneMid, stoneLarge, bug1;
	
	int rockSize;
	float probDiamond1, probDiamond2, probDiamond3, probDiamond4;
	
	bool AuxiliarVar;
	
	// Use this for initialization
	void Start () {
		
		AuxiliarVar = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if( GeneralController.Instance.startGameVar == true && AuxiliarVar == false){
			
			RandomAssetCreator();
			AuxiliarVar = true;
			
		}
	}
	
	void RandomAssetCreator(){
	
		//Instantiates the defined number of rocks large, medium, and small, as well as bugs in random locations across the blood diamond camp.
		for(int i = 0; i < GeneralController.Instance.NumberRocks; i++){
			
			rockSize = Random.Range(1, 4);
			
			if(rockSize == 1){
				Instantiate(stoneSmall, new Vector3(Random.Range(-8.0f, 45.0f), 22.0f, Random.Range(10.0f, 61.0f)), Quaternion.Euler(0.0f, 90.0f, 0.0f));
			}
			
			if(rockSize == 2){
				Instantiate(stoneMid, new Vector3(Random.Range(-8.0f, 45.0f), 25.0f, Random.Range(10.0f, 61.0f)), Quaternion.Euler(0.0f, 90.0f, 0.0f));
			}
			
			if(rockSize == 3){
				Instantiate(stoneLarge, new Vector3(Random.Range(-8.0f, 45.0f), 28.0f, Random.Range(10.0f, 61.0f)), Quaternion.Euler(0.0f, 90.0f, 0.0f));
			}
			
        }
		
		for(int j = 0; j < GeneralController.Instance.NumberBugs; j++){
			Instantiate(bug1, new Vector3(Random.Range(20.0f, 45.0f), 34.0f, Random.Range(-15.0f, 61.0f)), Quaternion.Euler(0.0f, 90.0f, 0.0f));
		}
	
	}
	
}