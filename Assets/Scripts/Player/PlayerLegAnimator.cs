using UnityEngine;

public class PlayerLegAnimator : MonoBehaviour {
    public GameObject legGroup;

    public Animator leftForwardLeg;
    public Animator rightForwardLeg;
    public Animator leftBackwardLeg;
    public Animator rightBackwardLeg;
    public float animationSpeed = .5f;

    public GameObject steamGroup;
    private ParticleSystem[] steams;
    
    public bool isSteamEmitting {
        set {
            foreach (ParticleSystem ps in steams) {
                ParticleSystem.EmissionModule emission = ps.emission;
                emission.enabled = value;
            }
        }
    }

    public bool areLegsActive {
        get { return legGroup.activeSelf; }
        set {
            legGroup.SetActive(value);
        }
    }

    private void Awake() {
        steams = steamGroup.GetComponentsInChildren<ParticleSystem>();
    }

    public void MoveForward(bool isForward, float speed) {
        leftForwardLeg.speed = speed * animationSpeed;
        rightForwardLeg.speed = speed * animationSpeed;
        leftBackwardLeg.speed = speed * animationSpeed;
        rightBackwardLeg.speed = speed * animationSpeed;

        leftForwardLeg.SetBool("cw", isForward);
        rightForwardLeg.SetBool("ccw", isForward);
        leftBackwardLeg.SetBool("cw", isForward);
        rightBackwardLeg.SetBool("ccw", isForward);
    }

    public void MoveBackWard(bool isBackward, float speed) {
        leftForwardLeg.speed = speed * animationSpeed;
        rightForwardLeg.speed = speed * animationSpeed;
        leftBackwardLeg.speed = speed * animationSpeed;
        rightBackwardLeg.speed = speed * animationSpeed;

        leftForwardLeg.SetBool("ccw", isBackward);
        rightForwardLeg.SetBool("cw", isBackward);
        leftBackwardLeg.SetBool("ccw", isBackward);
        rightBackwardLeg.SetBool("cw", isBackward);
    }
}
