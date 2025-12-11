using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 6000f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip fireSound;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }
    // used to triggerf animation to fire bullet
    public void PullTrigger()
    {
        gunAnimator.SetTrigger("Fire");
    }



//This function creates the bullet behavior
void Shoot()
{
    if (TargetAndAudioLoader.instance.shootSFX != null)
        source.PlayOneShot(TargetAndAudioLoader.instance.shootSFX);


    if (VRFXLoader.instance.muzzleFlashPrefab != null)
    {
        GameObject flash = Instantiate(
            VRFXLoader.instance.muzzleFlashPrefab,
            barrelLocation.position,
            barrelLocation.rotation
        );
        Destroy(flash, 2f);
    }


    if (!ProjectilePool.instance)
    {
        Debug.LogError("⚠ ProjectilePool is missing in the scene !");
        return;
    }

    // Get projectile from pool
    GameObject bullet = ProjectilePool.instance.GetBullet();

    bullet.transform.position = barrelLocation.position;
    bullet.transform.rotation = barrelLocation.rotation;

    

    bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
}



    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
