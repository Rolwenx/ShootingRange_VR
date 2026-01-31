using UnityEngine;

public class VRLocomotion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform xrRig; 
    [SerializeField] private Animator charAnimator;

    [Header("Settings")]
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float smooth = 5f;

    private Vector3 lastPosition;
    private float currentSpeed = 0f;

    void Start()
    {
        if (!xrRig)
            xrRig = transform;

        lastPosition = xrRig.position;
    }

    void Update()
    {
        Vector3 delta = xrRig.position - lastPosition;
        float rawSpeed = delta.magnitude / Time.deltaTime;

        float targetSpeed = rawSpeed * speedMultiplier;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smooth);

        charAnimator.SetFloat("speed", currentSpeed);

        lastPosition = xrRig.position;
    }

}
