using System.Collections.Generic;
using UnityEngine;

public class ProjectivePool : MonoBehaviour
{

    public GameObject bulletPrefab;
    public int initialPoolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewBullet();
        }
    }

    private GameObject CreateNewBullet()
    {
        GameObject obj = Instantiate(bulletPrefab);

        Bullet bulletScript = obj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(this);
        }

        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
        return obj;
    }

    public GameObject GetBullet()
    {
        if (pool.Count == 0)
            CreateNewBullet();

        GameObject bullet = pool.Dequeue();
        bullet.transform.SetParent(null);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;      // FIXED
            rb.angularVelocity = Vector3.zero;
        }

        bullet.transform.SetParent(transform);
        pool.Enqueue(bullet);
    }
}
