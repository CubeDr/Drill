using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Drill") {
            Destroy(gameObject);
        }
    }
}