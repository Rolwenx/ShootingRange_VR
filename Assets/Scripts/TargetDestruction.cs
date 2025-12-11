using UnityEngine;

public class TargetDestruction : MonoBehaviour
{
    public GameObject destructionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
            return;

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
