using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateContinuously : MonoBehaviour {

    public char rotateAxis;
    public float rotationPerMin;

    // Update is called once per frame
    void Update() {

        if (rotateAxis == 'x')
            transform.Rotate(6 * rotationPerMin * Time.deltaTime, 0, 0);
        else if (rotateAxis == 'y')
            transform.Rotate(0, 6 * rotationPerMin * Time.deltaTime, 0);
        else if (rotateAxis == 'z')
            transform.Rotate(0, 0, 6 * rotationPerMin * Time.deltaTime);

    }
}