using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableWeaponGrab : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // 🔥 Play equip animation (hand-agnostic)
        if (bossAnimator)
            bossAnimator.SetTrigger("Equip");

        // XR will automatically attach the weapon
        // to Left or Right socket depending on interactor
    }
}