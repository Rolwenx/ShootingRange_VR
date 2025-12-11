using UnityEngine;

public class TargetDestruction : MonoBehaviour
{
    public GameObject destructionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);

        TargetManager.instance.TargetDestroyed();

        gameObject.SetActive(false);
    }
}
