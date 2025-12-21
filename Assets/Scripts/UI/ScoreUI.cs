using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float refreshRate = 0.2f;

    private void Start()
    {
        StartCoroutine(UpdateScore());
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            scoreText.text = "Score : " + GameplayManager.Instance.GetScore();
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
