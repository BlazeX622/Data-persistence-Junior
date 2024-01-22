using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Make singleton
    public static ScoreManager instance;

    public float highscore;
    public string playerName;

    public string currentName;

    private void Awake()
    {
        // If duplicate, destroy
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // otherwise, create new singleton
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public float score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.score = highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscore = data.score;
            playerName = data.playerName;
        }
    }

    public void DeleteScore()
    {
        // Specify the file path
        string filePath = Application.persistentDataPath + "/savefile.json";

        // Check if the file exists before attempting to delete it
        if (File.Exists(filePath))
        {
            // Delete the file
            File.Delete(filePath);

            Debug.Log("Save file deleted successfully.");
        }
        else
        {
            Debug.LogWarning("Save file does not exist.");
        }

        highscore = 0;
        playerName = "NoName";
    }
}
