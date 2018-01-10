using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour {

    public float jumpPower;

    private Vector3 initialVelocity;
    private Rigidbody rb;
    private bool hitted = false;
    private float delay = 0f;

    private void OnTriggerStay(Collider other) {
        if (Time.time > delay) {
            if (other.gameObject.tag == "Player") {
                rb = GameObject.Find("Player").GetComponent<Rigidbody>();
                Vector3 direction = transform.up;
                direction *= jumpPower;
                rb.AddForce(direction, ForceMode.Impulse);
                print("hit");
                delay = Time.time + 0.2f;
            }
        }
    }



}