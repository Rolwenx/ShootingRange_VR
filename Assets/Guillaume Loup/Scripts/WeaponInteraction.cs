using UnityEngine;

public class WeaponInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Renderer weaponRenderer;
    [SerializeField] private Transform player; // XR Rig ou Camera
    [SerializeField] private Transform weaponSocket; // main du Boss

    [Header("Settings")]
    [SerializeField] private float hoverDistance = 1.5f;

    private MaterialPropertyBlock mpb;

    private static readonly int HoverID = Shader.PropertyToID("_Hover");
    private static readonly int GrabID  = Shader.PropertyToID("_Grab");

    private bool isEquipped = false;

    void Awake()
    {
        mpb = new MaterialPropertyBlock();
        if (!weaponRenderer)
            weaponRenderer = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        if (!weaponRenderer) return;

        weaponRenderer.GetPropertyBlock(mpb);

        if (!isEquipped)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            mpb.SetFloat(HoverID, dist <= hoverDistance ? 1f : 0f);
            mpb.SetFloat(GrabID, 0f);
        }
        else
        {
            mpb.SetFloat(HoverID, 0f);
            mpb.SetFloat(GrabID, 1f);
        }

        weaponRenderer.SetPropertyBlock(mpb);
    }

    // Appelé par ton Animation Event ou ton script d'equip
    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }
}