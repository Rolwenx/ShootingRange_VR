using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    private int activeHits = 0;

    private void Awake()
    {
        instance = this;
    }

    public void TargetDestroyed()
    {
        activeHits++;

        // when all targets have been hit:
        if (activeHits >= TargetPool.instance.spawnPoints.Length)
        {
            activeHits = 0;
            TargetPool.instance.SpawnAllTargets();
        }
    }
}
