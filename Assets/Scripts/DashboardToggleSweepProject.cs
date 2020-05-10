using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardToggleSweepProject : MonoBehaviour
{
    private HeadGestureSweep gesture;
    public GameObject dashboard;
    private bool isOpen = true;
    private Vector3 startRotation;

    //We are adding a two-seconds time delay
    // before deactivating the dashboard
    private float timer = 0.0f;
    private float timerReset = 2.0f;

    void Start()
    {
        gesture = GetComponent<HeadGestureSweep>();
        CloseDashboard();
    }

    void Update()
    {
        if (gesture.isRotatingDown)      //We check whether the user is in
            OpenDashboard();             //the RotatingDown gesture. If so,
                                         //we activate the dashboard

        else if (!gesture.isFacingDown)
        { //If not, we deactivate the dashboard
            timer -= Time.deltaTime;     //with a two-seconds time delay
            if (timer <= 0.0f)
                CloseDashboard();
        }
        else
            timer = timerReset;
    }

    private void CloseDashboard()
    {
        if (isOpen)
        {
            dashboard.SetActive(false);
            isOpen = false;
        }
    }

    private void OpenDashboard()
    {
        if (!isOpen)
        {
            dashboard.SetActive(true);
            isOpen = true;
        }
    }
}

