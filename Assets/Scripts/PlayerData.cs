using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public SettingsData settingsData;
    List<PlayerScore> playerScores = new List<PlayerScore>();
    string playerName;
    string bestPlayer;
    int currentScore;
    int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighscore();
        LoadSettingsData();
    }


    public void SetPlayerName(string s)
    {
        playerName = s;
    }

    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }

    public string GetBestPlayer()
    {
        return bestPlayer;
    }

    public List<PlayerScore> GetPlayerScoreList()
    {
        return playerScores;
    }

    public void SetBestScore(int score)
    {
        if (bestScore < score)
        {
            bestScore = score;
            bestPlayer = playerName;
        }
    }

    public void SetHighscores()
    {
        PlayerScore playerScore = new PlayerScore();
        playerScore.name = playerName;
        playerScore.score = currentScore;
        if (playerScores.Count == 0)
        {
            playerScores.Add(playerScore);
        }
        else
        {
            for (int i = 0; i < playerScores.Count; i++)
            {
                if (playerScore.score > playerScores[i].score)
                {
                    playerScores.Insert(i, playerScore);
                    return;
                }
            }
            if (playerScores.Count < 8)
            {
                playerScores.Add(playerScore);
            }
        }
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayer;
        public int bestScore;
        public List<PlayerScore> playerScores;
    }
    
    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.playerScores = playerScores;
        data.bestPlayer = bestPlayer;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savehighscore.json", json);
    }

    public void LoadHighscore()
    {
        string datapath = Application.persistentDataPath + "/savehighscore.json";
        if (File.Exists(datapath))
        {
            string json = File.ReadAllText(datapath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data.playerScores != null)
            playerScores = data.playerScores;
            bestPlayer = data.bestPlayer;
            bestScore = data.bestScore;
        }
    }

    public void SaveSettingsData()
    {
        string json = JsonUtility.ToJson(settingsData);
        File.WriteAllText(Application.persistentDataPath + "/savesettingsdata.json", json);
    }

    public void LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/savesettingsdata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(path);
            settingsData = JsonUtility.FromJson<SettingsData>(json);
        }
    }

}
