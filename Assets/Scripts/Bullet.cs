using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnEnable()
    {
        // auto-disable after 10 sec
        CancelInvoke();
        Invoke(nameof(Disable), 10f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
