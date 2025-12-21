using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    [Header("Pool Settings")]
    public GameObject bulletPrefab;
    public int poolSize = 20;

    [Header("Cleanup")]
    public float maxDistance = 50f;
    public float cleanupInterval = 0.3f;


    private Transform playerOrigin;
    private List<GameObject> pool = new List<GameObject>();
    private List<GameObject> activeProjectiles = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

     void Start()
    {
        playerOrigin = Camera.main.transform;

         // we create an initial pool of n bullets
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pool.Add(bullet);
        }

        StartCoroutine(Cleanup());
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
                // we register the current projectile in the list of active projectiles
                RegisterProjectile(bullet);
                return bullet;
            }
        }

        // if for whatever reason, the pool is all active, we add a new bullet to it
        GameObject newBullet = Instantiate(bulletPrefab);
        pool.Add(newBullet);
        RegisterProjectile(newBullet);
        return newBullet;
    }

    void RegisterProjectile(GameObject bullet)
    {
        if (!activeProjectiles.Contains(bullet))
            activeProjectiles.Add(bullet);
    }

    // we clean by deactivatine the bullets that surpasses the max distance
    IEnumerator Cleanup()
    {
        while (true)
        {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                GameObject bullet = activeProjectiles[i];

                if (!bullet.activeInHierarchy)
                {
                    activeProjectiles.RemoveAt(i);
                    continue;
                }

                float distance = Vector3.Distance(
                    bullet.transform.position,
                    playerOrigin.position
                );

                if (distance > maxDistance)
                {
                    Debug.Log("Bullet cleaned by pool");
                    bullet.SetActive(false);
                    activeProjectiles.RemoveAt(i);
                }

            }

            yield return new WaitForSeconds(cleanupInterval);
        }
    }

}
