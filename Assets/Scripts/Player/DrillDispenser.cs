using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDispenser : MonoBehaviour {
    public GameObject drill;
    public Transform drillPoint;
    public float maxDrillExposeDistance = 1;
    private Vector3 drillDefaultLocalPosition;

    private void Start() {
        drillDefaultLocalPosition = drill.transform.localPosition;
    }

    private void Update() {
        if (Input.GetMouseButton(0))
            ExposeDrill();
        else MoveDrillLocal(0);
    }

    void ExposeDrill() {
        RaycastHit hit;
        float distance = maxDrillExposeDistance;
        if (Physics.Raycast(drillPoint.position, transform.forward, out hit, maxDrillExposeDistance))
            distance = hit.distance;
        MoveDrillLocal(distance);
    }

    void MoveDrillLocal(float distance) {
        drill.transform.localPosition = drillDefaultLocalPosition + Vector3.forward * distance;
    }
}
