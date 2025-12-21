using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;    
    public float spawnInterval = 1.5f;

    private void Start()
    {
        StartCoroutine(WaitForPoolAndSpawn());
    }

    IEnumerator WaitForPoolAndSpawn()
    {
        while (!TargetPool.instance.isReady)
            yield return null;

        StartCoroutine(SpawnLoop());
    }


    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (GameplayManager.Instance.currentTargets <
                GameplayManager.Instance.maxTargets)
            {
                SpawnTargetFromPool();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTargetFromPool()
    {
        GameObject target = TargetPool.instance.GetTarget();

        if (target == null)
            return;

        Transform spawnPoint = spawnPoints[
            Random.Range(0, spawnPoints.Length)
        ];

        TargetPool.instance.ResetTarget(
            target,
            spawnPoint.position,
            spawnPoint.rotation
        );

        GameplayManager.Instance.RegisterTargetSpawn();
    }
}
