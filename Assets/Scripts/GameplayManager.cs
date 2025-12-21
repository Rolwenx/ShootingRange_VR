using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int currentScore = 0;

    public static GameplayManager Instance; 

    
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
}
