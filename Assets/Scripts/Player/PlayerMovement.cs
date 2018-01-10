using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerLegAnimator))]
[RequireComponent(typeof(Roll))]
public class PlayerMovement : MonoBehaviour {
    public float speed=2;
    public float backMultiplier = .7f;
    public float friction = .1f;
    public float brakeThreshold = .2f;

    public Transform legBottom;

    [HideInInspector]
    public bool movable = true;
    [HideInInspector]
    public bool braking = false;

    private new Rigidbody rigidbody;
    private PlayerLegAnimator animator;
    private Roll roll;

    private Vector3 _lastGroundNormal = Vector3.up;
    public Vector3 lastGroundNormal {
        get {
            return _lastGroundNormal;
        }
        set {
            _lastGroundNormal = value;
            roll.up = value;
        }
    }

    public bool isLegOut {
        get {
            return animator.areLegsActive;
        }
        set {
            if (value) {
                StartBraking();
            }
            roll.affectable = !value;
            animator.areLegsActive = value;
        }
    }

    public bool IsGrounded {
        get {
            if (isLegOut) {
                RaycastHit hit;
                if (Physics.Raycast(new Ray(legBottom.position, -legBottom.up), out hit, .1f, (1 << 8) | (1 << 9))) {
                    return true;
                }
            } else {
                RaycastHit hit;
                if (Physics.Raycast(new Ray(transform.position, -_lastGroundNormal), out hit, .6f, (1 << 8) | (1 << 9))) {
                    return true;
                }
            }
            return false;
        }
    }

    private Vector3 lastVelocity = Vector3.zero;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerLegAnimator>();
        roll = GetComponent<Roll>();
    }

    void Start() {
        roll.affectable = false;    
    }

    void Update () {
        if(!isLegOut) {
            roll.affectable = IsGrounded;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            isLegOut = !isLegOut;
        }
	}

    void FixedUpdate() {
        if (braking || !IsGrounded) return;
        if(isLegOut && movable) {
            Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity);
            localVelocity.x = localVelocity.z = 0;
            float s = Input.GetAxis("Vertical") * speed;
            if (s > 0) {
                animator.MoveBackWard(false, 0);
                animator.MoveForward(true, s);
            } else if (s < 0) {
                s *= backMultiplier;
                animator.MoveBackWard(true, 0);
                animator.MoveForward(false, -s);
            } else {
                animator.MoveForward(false, 0);
                animator.MoveBackWard(false, 0);
            }
            localVelocity.z = s;
            rigidbody.velocity = transform.TransformDirection(localVelocity);
        }
    }

    public void StartBraking() {
        if(!isLegOut) {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z = 0;
            transform.rotation = Quaternion.Euler(rotation);

            transform.Translate(transform.up * .2f);
        }
        StartCoroutine("StartBrakingCoroutine");
    }

    IEnumerator StartBrakingCoroutine() {
        animator.isSteamEmitting = true;
        braking = true;

        while(rigidbody.velocity != Vector3.zero) {
            if (rigidbody.velocity.magnitude <= brakeThreshold) break;
            rigidbody.velocity *= (1 - friction);
            yield return new WaitForSeconds(.1f);
        }
        rigidbody.velocity = Vector3.zero;
        braking = false;
        animator.isSteamEmitting = false;
    }

    private void OnCollisionStay(Collision collision) {
        if (!isLegOut) {
            ContactPoint[] points = collision.contacts;
            lastGroundNormal = points[0].normal;
            print("LastNormal : " + lastGroundNormal);
        }
    }
}
