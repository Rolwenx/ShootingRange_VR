using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TableWeaponGrab : MonoBehaviour
{
    [SerializeField] private GameObject equippedWeaponPrefab;
    [SerializeField] private Transform weaponSocket;
    [SerializeField] private Animator bossAnimator;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Animation
        bossAnimator.SetTrigger("Equip");

        // Spawn arme équipée
        GameObject weapon = Instantiate(
            equippedWeaponPrefab,
            weaponSocket
        );

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        // Supprimer l’arme table
        gameObject.SetActive(false);
    }
}