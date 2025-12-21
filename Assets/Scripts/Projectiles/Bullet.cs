using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        // collisions may happen because of the weapon or other bullets so ignore them
        if (other.CompareTag("Bullet") || other.CompareTag("Weapon"))
            return;

        // disable only on valid impact
        gameObject.SetActive(false);
    }
}
