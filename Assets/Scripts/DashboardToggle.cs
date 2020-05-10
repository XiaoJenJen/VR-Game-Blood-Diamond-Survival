using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardToggle : MonoBehaviour
{
    private HeadGesture gesture; //A variable to get if the user is facing down
                                 //and rotating down from the HeadGesture class (script)
    public GameObject dashboard;
    private bool isOpen = true;
    private Vector3 startRotation;

    void Start()
    {
        gesture = GetComponent<HeadGesture>();
        CloseDashboard();
    }

    void Update()
    {
        if (gesture.isFacingDown)    //We check whether the user is in isFacingDown gesture
            OpenDashboard();         //If so, we activate the dashboard
        else
            CloseDashboard();        //If not, the dashboard is kept deactivated
    }

    private void CloseDashboard()
    {   //We deactivate the dashboard, if it is active
        if (isOpen)
        {
            dashboard.SetActive(false);
            isOpen = false;
        }
    }

    private void OpenDashboard()
    {    //We activate the dashboard, if it not active
        if (!isOpen)
        {
            dashboard.SetActive(true);
            isOpen = true;
        }
    }
}

