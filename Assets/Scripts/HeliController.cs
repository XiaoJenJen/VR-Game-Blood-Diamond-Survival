using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour {

    public Transform[] pointTransforms;
    private int currentPosition;
    void Start()
    {
        currentPosition = 0;
        StartCoroutine(HeliMove());
    }


    IEnumerator HeliMove()
    {
        while (true)
        {
            Vector3 startPosition = pointTransforms[currentPosition].position;
            Vector3 endPosition = pointTransforms[(currentPosition + 1) % pointTransforms.Length].position;

            Quaternion startRotation = pointTransforms[currentPosition].rotation;
            Quaternion endRotation = pointTransforms[(currentPosition + 1) % pointTransforms.Length].rotation;

            for (float f = 0.0f; f < 1.0f; f += Time.deltaTime / 8.0f)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, f);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, f);
                yield return null; //To make Helicopter's animation visible
            }

            //To make the Helicopter go back to the first point
            transform.position = Vector3.Lerp(startPosition, endPosition, 1.0f);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, 1.0f);

            //To increase the current position by one
            currentPosition = (currentPosition + 1) % pointTransforms.Length;
        }
    }
}
