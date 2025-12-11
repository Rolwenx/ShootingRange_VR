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



        GameObject fx = Instantiate(
        VRFXLoader.instance.impactPrefab, 
        transform.position, 
        Quaternion.identity
        );

        ParticleSystem ps = fx.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        Destroy(fx, 2f);




        TargetManager.instance.TargetDestroyed();

        transform.root.gameObject.SetActive(false);
    }
}
