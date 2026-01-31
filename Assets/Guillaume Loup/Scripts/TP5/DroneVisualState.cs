using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DroneVisualState : MonoBehaviour
{
    public Renderer rend;
    MaterialPropertyBlock mpb;
    public DroneFollower drone;
    private float risk;

    private static readonly int RiskID = Shader.PropertyToID("_Risk");


    void Awake()
    {
        mpb = new MaterialPropertyBlock();
    }

    void LateUpdate()
    {
        if(drone.currentMode == false)
        {
            risk = 1;
        }
        else
        {
            risk = 0;
        }
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat(RiskID, risk);

        rend.SetPropertyBlock(mpb);
    }
}