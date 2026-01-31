using UnityEngine;

public class DroneAnimState : MonoBehaviour
{
    public DroneFollower follower; 
    Animator anim;
    static readonly int IsCautiousID = Animator.StringToHash("IsCautious");

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (!follower) follower = GetComponentInParent<DroneFollower>();
    }

    void LateUpdate()
    {
        // Ton bool : false = cautious
        bool isCautious = follower.currentMode == false;
        anim.SetBool(IsCautiousID, isCautious);
    }
}