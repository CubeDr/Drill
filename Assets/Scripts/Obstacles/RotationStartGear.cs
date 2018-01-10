using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStartGear : MonoBehaviour {

    public float keySpeed;
    public GameObject[] connectedObjects;

    private bool contactDrill = false;
    private RotateContinuously RC;
    private RotateContinuously obj_RC;


    // Use this for initialization
    void Start() {

        RC = GetComponent<RotateContinuously>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Drill" /*&& player.rotateVelocity.magnitude > keySpeed*/) {
            if (!RC.enabled) {
                RC.enabled = true;

                foreach (GameObject obj in connectedObjects) {
                    obj_RC = obj.GetComponent<RotateContinuously>();
                    if (obj_RC != null)
                        obj_RC.enabled = true;
                }
            } else {
                RC.enabled = false;

                foreach (GameObject obj in connectedObjects) {
                    obj_RC = obj.GetComponent<RotateContinuously>();
                    if (obj_RC != null)
                        obj_RC.enabled = false;
                }
            }

            //player.rotateVelocity = Vector3.zero;
        }
    }

}