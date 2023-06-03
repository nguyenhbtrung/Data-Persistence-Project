using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    string bestScore;
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        bestScore = text.text;
    }
    private void Update()
    {
        if (PlayerData.Instance != null)
        {
            bestScore = "Best Score:" + PlayerData.Instance.GetBestPlayer() +":" + PlayerData.Instance.GetBestScore();
        }
        text.SetText(bestScore);
    }
}
