using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class HighscoreTable : MonoBehaviour
{
    Transform entryContainer;
    GameObject valueContainerTemplate;
    List<GameObject> valueContainers = new List<GameObject>();
    List<PlayerScore> playerScores;

    private void Awake()
    {
        valueContainerTemplate = GameObject.Find("ValueContainerTemplate");
        entryContainer = transform.Find("EntryContainer");
        valueContainerTemplate.SetActive(false);
        playerScores = PlayerData.Instance.GetPlayerScoreList();

        for (int i = 0; i < 8; i++)
        {
            GameObject valueContainer = Instantiate(valueContainerTemplate, entryContainer);
            valueContainer.SetActive(true);
            TextMeshProUGUI rankText = valueContainer.transform.Find("RankText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = valueContainer.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameText = valueContainer.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            if (i < playerScores.Count)
            {
                switch (i)
                {
                    default: rankText.SetText((i + 1).ToString() + "TH"); break;
                    case 0: rankText.SetText("1ST"); break;
                    case 1: rankText.SetText("2ND"); break;
                    case 2: rankText.SetText("3RD"); break;
                }

                scoreText.text = playerScores[i].score.ToString();
                nameText.text = playerScores[i].name;

            }

            if (i % 2 != 0)
            {
                valueContainer.transform.Find("ValueContainerBackground").gameObject.SetActive(false);
            }
            valueContainers.Add(valueContainer);
        }
    }
}
