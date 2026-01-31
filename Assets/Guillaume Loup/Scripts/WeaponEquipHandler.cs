using UnityEngine;

public class WeaponEquipHandler : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Visual or model of the weapon (already grabbed by XR)")]
    public GameObject weaponVisual;

    public void OnEquipAnimationEvent()
    {
        if (!weaponVisual) return;

        // Just reveal / activate
        weaponVisual.SetActive(true);
    }
}