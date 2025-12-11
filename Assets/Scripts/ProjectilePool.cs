using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    [Header("Pool Settings")]
    public GameObject bulletPrefab;
    public int poolSize = 15;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // we create the initial pool of poolSize
        for (int i = 0; i < poolSize; i++)
        {
            // we add a bullet to the pool and we directly put it unactive
            GameObject _bullet = Instantiate(bulletPrefab);
            _bullet.SetActive(false);
            pool.Add(_bullet);
        }
    }

    public GameObject GetBullet()
    {
        // we search for an inactive bullet in the pool
        foreach (GameObject bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        // if, for whatever reason, the pool becomes empty, we expand it again.
        GameObject newBullet = Instantiate(bulletPrefab);
        pool.Add(newBullet);
        return newBullet;
    }
}
