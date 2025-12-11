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
            GameObject _tempTarget = Instantiate(targetPrefab);
            _tempTarget.SetActive(false);
            pool.Add(_tempTarget);
        }

        // on place les premières cibles dans la scène
        SpawnAllTargets();
    }

    // on retourne une cible inactive du pool, et s'il y en a pas, on en crée une nouvelle
    public GameObject GetTarget()
    {
        foreach (GameObject t in pool)
        {
            // si cible inactive, on la reutilise
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
    // on fait apparaitre une cible sur les points de spawn
    public void SpawnAllTargets()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject t = GetTarget();
            ResetTarget(t, point.position, point.rotation);
        }
    }

    // on réinitialise l’état d’une cible avant réactivation
    public void ResetTarget(GameObject target, Vector3 position, Quaternion rotation)
    {
        target.transform.position = position;
        target.transform.rotation = rotation;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Collider col = target.GetComponent<Collider>();
        if (col)
            col.enabled = true;

        target.SetActive(true);
    }
}
