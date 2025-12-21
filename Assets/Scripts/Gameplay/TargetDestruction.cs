using UnityEngine;

public class TargetDestruction : MonoBehaviour
{
    public GameObject destructionEffect;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip targetImpact;
    public int ScoreToAdd = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        GameplayManager.Instance.AddScore(ScoreToAdd);
        GameplayManager.Instance.RegisterTargetDespawn();
            
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

        transform.root.gameObject.SetActive(false);
    }
}
