using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    public static TargetPool instance;

    [Header("Pool Settings")]
    public GameObject targetPrefab;
    public int poolSize = 10;

    private List<GameObject> pool = new List<GameObject>();
    public bool isReady = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitForTargetPrefab());
    }

    private IEnumerator WaitForTargetPrefab()
    {
        // attendre que TargetAndAudioLoader ait chargé la target
        while (!TargetAndAudioLoader.instance.isReady ||
               TargetAndAudioLoader.instance.targetPrefab == null)
        {
            yield return null; // attendre un frame
        }

        // maintenant on peut créer le pool
        targetPrefab = TargetAndAudioLoader.instance.targetPrefab;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject temp = Instantiate(targetPrefab);
            temp.SetActive(false);
            pool.Add(temp);
        }

        isReady = true;
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
