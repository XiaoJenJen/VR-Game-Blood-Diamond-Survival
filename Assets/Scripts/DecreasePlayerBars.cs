using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreasePlayerBars : MonoBehaviour
{

    private float Hunger, Thirst, DeathTime, DeathTimeMax;
	private int Debt;
    public Image hungerBar;
	public Image thirstBar;
	public Text debtText;
	public Text debtTextDieBoard;
	public Text deathtimerText;
	
    void Start(){
		
		//Set initial parameters to dashboard
		DeathTimeMax = GeneralController.Instance.DeathTimer;
		deathtimerText.text = "" + DeathTimeMax;
		
		Debt = GeneralController.Instance.WarlordDebt;
		debtText.text = "" + Debt;
		debtTextDieBoard.text = "" + Debt;
		
		Hunger = GeneralController.Instance.HungryNumber;
		
		Thirst = GeneralController.Instance.ThristNumber;


    }

    void Update(){
		
		DeathTime = DeathTimeMax - GeneralController.Instance.gameTimer;
		deathtimerText.text = "" + (int)DeathTime;
		
		
		// Updates the Ward lord's Debt
		if(Debt != GeneralController.Instance.WarlordDebt){ 
			Debt = GeneralController.Instance.WarlordDebt;
			debtText.text = "" + Debt;
			debtTextDieBoard.text = "" + Debt;
		}
		
		// Updates the hunger bar
		if(Hunger != GeneralController.Instance.HungryNumber){
			
			Hunger = GeneralController.Instance.HungryNumber;
			hungerBar.fillAmount = Hunger/GeneralController.Instance.MaxHungerBar;
			
		}
		
		// Updates the thirst bar
		if(Thirst != GeneralController.Instance.ThristNumber){
			
			Thirst = GeneralController.Instance.ThristNumber;
			thirstBar.fillAmount = (Thirst / GeneralController.Instance.MaxThirstBar);
			
		}
	}

}

