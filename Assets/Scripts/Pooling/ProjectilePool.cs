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
        // we create an initial pool of n bullets
        for (int i = 0; i < poolSize; i++)
        {
            GameObject _tempBullet = Instantiate(bulletPrefab);
            _tempBullet.SetActive(false);
            pool.Add(_tempBullet);
        }
    }

    public GameObject GetBullet()
    {
        // we search a bullet that is inactive, to make it active
        foreach (GameObject bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                // when we find one, we set it active
                bullet.SetActive(true);
                return bullet;
            }
        }

        // if for whatever reason, the pool is all active, we add a new bullet to it
        GameObject newBullet = Instantiate(bulletPrefab);
        pool.Add(newBullet);
        return newBullet;
    }
}
