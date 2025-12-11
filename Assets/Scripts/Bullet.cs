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

        // Auto-disable after 10 sec
        Invoke(nameof(Disable), 10f);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        // Optional: disable on impact
        StartCoroutine(nameof(Disable));
    }
}
