using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[DisallowMultipleComponent]
public class WeaponHighlightMPB : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable; 
    [SerializeField] private Renderer targetRenderer; 
    [Header("Smoothing")]
    // vitesse de transition du changement 
    [SerializeField] private float hoverSmooth = 12f; 
    [SerializeField] private float grabSmooth  = 18f;

    private static readonly int HoverID = Shader.PropertyToID("_Hover");
    private static readonly int GrabID  = Shader.PropertyToID("_Grab");

    private MaterialPropertyBlock mpb;

    private float hoverTarget = 0f;
    private float grabTarget  = 0f;
    private float hoverValue  = 0f;
    private float grabValue   = 0f;

    private void Reset()
    {
        // Auto-find
        if (!interactable) interactable = GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (!targetRenderer) targetRenderer = GetComponentInChildren<Renderer>();
    }

    private void Awake()
    {
        if (!interactable) interactable = GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (!targetRenderer) targetRenderer = GetComponentInChildren<Renderer>();

        mpb = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
        if (!interactable) return;

        // hover 
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);

        // Grab (select)
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        if (!interactable) return;

        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
        interactable.selectEntered.RemoveListener(OnSelectEntered);
        interactable.selectExited.RemoveListener(OnSelectExited);
    }

    private void Update()
    {
        if (!targetRenderer) return;

        // on fait des transitions smooth
        hoverValue = Mathf.MoveTowards(hoverValue, hoverTarget, hoverSmooth * Time.deltaTime);
        grabValue  = Mathf.MoveTowards(grabValue,  grabTarget,  grabSmooth  * Time.deltaTime);

        targetRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat(HoverID, hoverValue);
        mpb.SetFloat(GrabID,  grabValue);
        targetRenderer.SetPropertyBlock(mpb);
    }

    private void OnHoverEntered(HoverEnterEventArgs args) => hoverTarget = 1f;
    private void OnHoverExited(HoverExitEventArgs args)   => hoverTarget = 0f;

    private void OnSelectEntered(SelectEnterEventArgs args) => grabTarget = 1f;
    private void OnSelectExited(SelectExitEventArgs args)   => grabTarget = 0f;
}
