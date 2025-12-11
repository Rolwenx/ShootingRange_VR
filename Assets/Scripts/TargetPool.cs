using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    public static TargetPool instance;

    [Header("Pool Settings")]
    public GameObject targetPrefab;
    public int poolSize = 10;
    public Transform[] spawnPoints;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // initial target pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(targetPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }

        SpawnAllTargets();
    }

    public GameObject GetTarget()
    {
        foreach (GameObject t in pool)
        {
            if (!t.activeInHierarchy)
            {
                return t;
            }
        }

        GameObject newTarget = Instantiate(targetPrefab);
        newTarget.SetActive(false);
        pool.Add(newTarget);
        return newTarget;
    }

    public void SpawnAllTargets()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject t = GetTarget();
            ResetTarget(t, point.position, point.rotation);
        }
    }

    public void ResetTarget(GameObject target, Vector3 position, Quaternion rotation)
    {
        target.transform.position = position;
        target.transform.rotation = rotation;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Collider col = target.GetComponent<Collider>();
        if (col)
            col.enabled = true;

        target.SetActive(true);
    }
}
