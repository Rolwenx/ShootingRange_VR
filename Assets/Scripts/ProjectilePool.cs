using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    [Header("Pool Settings")]
    public GameObject bulletPrefab;
    public int poolSize = 20;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // creation of initial pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject _bullet = Instantiate(bulletPrefab);
            _bullet.SetActive(false);
            pool.Add(_bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject _bullet in pool)
        {
            if (!_bullet.activeInHierarchy)
                return _bullet;
        }

        // if all bullets busy, we extend the pool to not be stuck
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        pool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
