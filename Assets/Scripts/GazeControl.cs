using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeControl : MonoBehaviour
{
    public void GazeEnter()
    {
        //We are changing the material color to red
        GetComponent<Renderer>().material.color = Color.red;

        //We are enabling the outline on hover
        transform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
    }

    public void GazeExit()
    {
        //We are changing the material color to blue
        GetComponent<Renderer>().material.color = Color.blue;

        //We are disabling the outline
        transform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
    }
}


