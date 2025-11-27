using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public GameObject targetPrefab;
    public Transform[] spawnPoints;

    private int destroyedCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnTargets();
    }

    public void TargetDestroyed()
    {
        destroyedCount++;

        if (destroyedCount >= spawnPoints.Length)
        {
            destroyedCount = 0;
            SpawnTargets();
        }
    }

    void SpawnTargets()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(targetPrefab, point.position, point.rotation);
        }
    }
}
