using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelPanel : MonoBehaviour {

    public float accelPower;

    void OnTriggerStay(Collider other) {
        print(other.tag);
        if (other.tag == "Player") {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            player.movable = false;

            Vector3 direction = gameObject.transform.forward.normalized;
            direction *= accelPower;


            Rigidbody playerRigidBody = player.GetComponentInParent<Rigidbody>();
            if (player.isLegOut) playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.AddForce(direction, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();

            if (!player.isLegOut) return;
            player.movable = true;
            player.StartCoroutine("StartBraking");
        }
    }
}