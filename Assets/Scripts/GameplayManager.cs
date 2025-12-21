using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int currentScore = 0;

    public static GameplayManager Instance; 

    [Header("Targets")]
    public int maxTargets = 4;
    public int currentTargets = 0;

    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
     public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public int GetScore()
    {
        return currentScore;
    } 
    public void RegisterTargetSpawn()
    {
        currentTargets++;
    }

    public void RegisterTargetDespawn()
    {
        currentTargets--;
        if (currentTargets < 0)
            currentTargets = 0;
    }
}
