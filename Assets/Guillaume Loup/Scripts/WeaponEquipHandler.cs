using UnityEngine;

public class WeaponEquipHandler : MonoBehaviour
{
    [Header("References")]
    public Transform weaponSocket; 
    
    public GameObject weaponVisual; 
    
    public void AttachWeapon()
    {
        if (!weaponSocket || !weaponVisual) return;

        weaponVisual.transform.SetParent(weaponSocket);
        weaponVisual.transform.localPosition = Vector3.zero;
        weaponVisual.transform.localRotation = Quaternion.identity;

        weaponVisual.SetActive(true);
    }
}
