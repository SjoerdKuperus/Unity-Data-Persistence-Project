using System;
using System.IO;
using UnityEngine;

public class SharedSceneDataSingleton : MonoBehaviour
{
    static public SharedSceneDataSingleton Instance;
    public HighScore LastHighScore;
    public string PlayerName;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPersitentData();
    }

    public void SavePersitentData()
    {
        PersistentData data = new PersistentData
        {
            LastHighScore = LastHighScore,
            PlayerName = PlayerName
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPersitentData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PersistentData data = JsonUtility.FromJson<PersistentData>(json);
            LastHighScore = data.LastHighScore;
            PlayerName = data.PlayerName;
        }
    }

    /// <summary>
    /// Checks if the point total is higher then the last highscore.
    /// If higher, update the persistent data.
    /// </summary>
    public void UpdateOrIgnoreHighScore(int m_Points)
    {
        if(LastHighScore != null)
        {
            if(LastHighScore.Score < m_Points)
            {
                //New highscore
                var newHighScore = new HighScore
                {
                    Name = PlayerName,
                    Score = m_Points
                };
                LastHighScore = newHighScore;
                SavePersitentData();
            }
        }
        else
        {
            //First highscore
            var firstHighScore = new HighScore
            {
                Name = PlayerName,
                Score = m_Points
            };
            LastHighScore = firstHighScore;
            SavePersitentData();
        }        
    }

    public string GetLastHighScoreText()
    {
        if (LastHighScore == null || LastHighScore.Score == 0)
        {
            return "Highscore: ???";
        }
        return $"Highscore: {LastHighScore.Score} by {LastHighScore.Name}";
    }
}

[System.Serializable]
public class PersistentData
{
    public HighScore LastHighScore;
    public string PlayerName;
}

[System.Serializable]
public class HighScore
{
    public string Name;
    public int Score;
}