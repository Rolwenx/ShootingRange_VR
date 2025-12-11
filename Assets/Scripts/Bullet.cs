using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;

    // Pool reference assigned by the ProjectivePool
    private ProjectivePool bulletPool;

    // Called by the pool when the bullet is created
    public void Initialize(ProjectivePool pool)
    {
        bulletPool = pool;
    }

    private void OnEnable()
    {
        // Start lifetime timer
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void OnDisable()
    {
        // Cancel the timer when the bullet is disabled
        CancelInvoke();
    }

    // Removed OnCollisionEnter → you handle collision somewhere else

    private void ReturnToPool()
    {
        if (!gameObject.activeSelf) return;

        if (bulletPool != null)
        {
            bulletPool.ReturnBullet(gameObject);
        }
        else
        {
            // Safety fallback
            Destroy(gameObject);
        }
    }
}
