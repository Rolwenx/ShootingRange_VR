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
        // Create initial pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetBullet()
    {
        // Look for an inactive bullet
        foreach (GameObject bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                Debug.Log("from pool");
                Debug.Log(bullet.activeInHierarchy);
                bullet.SetActive(true);
                Debug.Log(bullet.activeInHierarchy);
                return bullet;
            }
        }

        // Optional: expand pool if empty
        GameObject newBullet = Instantiate(bulletPrefab);
        pool.Add(newBullet);
        Debug.Log("added");
        return newBullet;
    }
}
