using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMove : MonoBehaviour
{
	//Added a smokeCamp particle system that activates once you grab a diamond to help you find the base camp
	public GameObject smokeCamp;
	
    private bool isGrabbing = false;
    private Transform grabbedTransform;
    public float zSpeed = 2.0f;
    private Transform hitTransform;
	
	void Start(){	
	
		//Used to deactivate box collider for hand for trigger to hit rock
		GetComponentInChildren<Collider>().enabled = false;
		
	}
	
    void Update()
    {
		
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        transform.localPosition = OVRInput.GetLocalControllerPosition(activeController);
        transform.localRotation = OVRInput.GetLocalControllerRotation(activeController);

        RaycastHit hitInfo2;
		//Parameters of the Physics.Raycast(position, direction, and lentght) = Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo2, 3.0f)
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo2, 2.5f))
        {

			//Used to highlight the difficulty buttons
            if ( (hitInfo2.transform.tag == "DifficultyEasy" && !isGrabbing) || (hitInfo2.transform.tag == "DifficultyMed" && !isGrabbing)  || (hitInfo2.transform.tag == "DifficultyHard" && !isGrabbing) )
			{
                
				if (hitTransform != null)
                    SetHighlightWater(hitTransform, false);

                hitTransform = hitInfo2.transform;
                SetHighlightWater(hitTransform, true);
            } 
			//Used to identify if a grabbable object is a bug 
            else  if (hitInfo2.transform.tag == "Bug" && !isGrabbing)
            {
                if (hitTransform != null)
                    SetHighlight(hitTransform, false);

                hitTransform = hitInfo2.transform;
                SetHighlight(hitTransform, true);
            }
			//Used to identify if an interactive object is water
			else if (hitInfo2.transform.tag == "Water" && !isGrabbing)
            {
                if (hitTransform != null)
                    SetHighlightWater(hitTransform, false);

                hitTransform = hitInfo2.transform;
                SetHighlightWater(hitTransform, true);
            }
			//Used to identify if an grabbable object is a diamond
			else if ((hitInfo2.transform.tag == "Diamond1" && !isGrabbing) || (hitInfo2.transform.tag == "Diamond2" && !isGrabbing) || (hitInfo2.transform.tag == "Diamond3" && !isGrabbing))
            {
                if (hitTransform != null)
                    SetHighlightDiamond(hitTransform, false);

                hitTransform = hitInfo2.transform;
                SetHighlightDiamond(hitTransform, true);
            }
			//Used to identify if an grabbable object is a rockstalk
			else if(hitInfo2.transform.tag == "RockStalk" && !isGrabbing)
            {
                if (hitTransform != null)
                    SetHighlightInteractive(hitTransform, false);

                hitTransform = hitInfo2.transform;
                SetHighlightInteractive(hitTransform, true);
            }
            else
            {
                if (hitTransform != null && !isGrabbing)
                    SetHighlight(hitTransform, false);
            }
			
			//Used for Interactive Objects
			if (hitInfo2.transform.tag == "Interactive" && !isGrabbing)
            {
                hitTransform = hitInfo2.transform;
                SetHighlightInteractive(hitTransform, true);
            }
			
        }
        else
        {
            if (hitTransform != null && !isGrabbing)
            {
                SetHighlight(hitTransform, false);
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 2.5f))
            {
				
                if ( hitInfo2.transform.tag == "DifficultyEasy" || hitInfo2.transform.tag == "DifficultyMed"  || hitInfo2.transform.tag == "DifficultyHard" ){
				
					if ( hitInfo2.transform.tag == "DifficultyEasy" ) GeneralController.Instance.DifficultyGame = 1;
					if ( hitInfo2.transform.tag == "DifficultyMed" ) GeneralController.Instance.DifficultyGame = 2;
					if ( hitInfo2.transform.tag == "DifficultyHard" ) GeneralController.Instance.DifficultyGame = 3;
				
				}
				//If a bug is grabbed, it gets destroyed and the HungerBar is raised to 15 again
				else if (hitInfo.transform.tag == "Bug")
                {
                    Destroy(hitInfo.transform.gameObject);
					GeneralController.Instance.HungryNumber = 15;
					
                }
				//If a Water is grabbed, it gets destroyed and the ThirstBar is raised to 20 again
                else if (hitInfo.transform.tag == "Water")
                {
					
					GeneralController.Instance.ThristNumber = 20;
					
                }
				else if (hitInfo.transform.tag == "Diamond1" || hitInfo.transform.tag == "Diamond2" || hitInfo.transform.tag == "Diamond3")
                {
                    isGrabbing = true;
                    grabbedTransform = hitInfo.transform;
                    grabbedTransform.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedTransform.GetComponent<Rigidbody>().useGravity = false;
                    grabbedTransform.parent = transform;
					
					//Activates smoke in camp as soon as you pick a diamond
					smokeCamp.SetActive(true);
                }
				else if (hitInfo.transform.tag == "RockStalk")
                {
                    isGrabbing = true;
                    grabbedTransform = hitInfo.transform;
                    grabbedTransform.transform.localRotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
                    grabbedTransform.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedTransform.GetComponent<Rigidbody>().useGravity = false;
                    grabbedTransform.parent = transform;
					
					//Used to activate box collider for hand for trigger to hit rock
					GetComponentInChildren<Collider>().enabled = true;
					
                }
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (grabbedTransform != null)
            {
                grabbedTransform.GetComponent<Rigidbody>().isKinematic = false;
                grabbedTransform.GetComponent<Rigidbody>().useGravity = true;
                grabbedTransform.parent = null;
            }
			
            isGrabbing = false;
			
			
			//Used to deactivate box collider for hand for trigger to hit rock
			GetComponentInChildren<Collider>().enabled = false;
			
			//Deactivates smoke in camp as soon as you release a diamond
			smokeCamp.SetActive(false);
        }

    }
	
	//Used for changing color of grabbable objects
    void SetHighlight(Transform t, bool highlight)
    {
        if (highlight)
        {
            hitTransform.GetComponent<Renderer>().material.color = Color.cyan;
            hitTransform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            t.GetComponent<Renderer>().material.color = t.GetComponent<IsHit_S>().originalColorVar;
            t.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        }
    }
	
	//Used for changing color of interactive not grabbable objects
	void SetHighlightInteractive(Transform t, bool highlight)
    {
        if (highlight)
        {
            hitTransform.GetComponent<Renderer>().material.color = Color.white;
            hitTransform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            t.GetComponent<Renderer>().material.color = t.GetComponent<IsHit_S>().originalColorVar;
            t.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        }
    }
	
	//Used for changing color of Diamonds
	void SetHighlightDiamond(Transform t, bool highlight)
    {
        if (highlight)
        {
            hitTransform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            t.GetComponent<Renderer>().material.color = t.GetComponent<IsHit_S>().originalColorVar;
            t.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        }
    }
	
	//Used for changing color of Water
	void SetHighlightWater(Transform t, bool highlight)
    {
        if (highlight)
        {
            hitTransform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            t.GetComponent<Renderer>().material.color = t.GetComponent<IsHit_S>().originalColorVar;
            t.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        }
    }
	
}
