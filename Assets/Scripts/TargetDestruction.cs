using UnityEngine;

public class TargetDestruction : MonoBehaviour
{
    public GameObject destructionEffect;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip targetImpact;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
            return;
            
        AudioSource.PlayClipAtPoint(
        TargetAndAudioLoader.instance.impactSFX,
        transform.position);


       if (VRFXLoader.instance.impactPrefab != null)
        {
            GameObject fx = Instantiate(
                VRFXLoader.instance.impactPrefab, 
                transform.position, 
                Quaternion.identity
            );

            Destroy(fx, 2f); 
        }


        TargetManager.instance.TargetDestroyed();

        gameObject.SetActive(false);
    }
}
