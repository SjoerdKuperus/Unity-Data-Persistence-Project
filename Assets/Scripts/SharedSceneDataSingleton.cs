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

}

[System.Serializable]
public class PersistentData
{
    public HighScore LastHighScore;
    public string PlayerName;
}

public class HighScore
{
    public string Name;
    public int Score;
}