using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIntroBoard : MonoBehaviour {
	
    public GameObject introBoard, buttonEasy, buttonMed, buttonHard;
    private bool introBoardActive;

    void Start()
    {
        introBoardActive = true;
		introBoard.SetActive(true);
		buttonEasy.SetActive(true);
		buttonMed.SetActive(true);
		buttonHard.SetActive(true);
    }

    void Update()
    {
		//Used for activating and deactivating the introduction board when required
        if (introBoardActive == true && GeneralController.Instance.DifficultyGame > 0)
        {
 
			buttonEasy.SetActive(false);
			buttonMed.SetActive(false);
			buttonHard.SetActive(false);
			introBoard.SetActive(false);
			introBoardActive = false;
            
        }
    }

}