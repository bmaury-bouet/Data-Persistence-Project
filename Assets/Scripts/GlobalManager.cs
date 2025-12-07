using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;


public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    public string PlayerName { get; private set; }
    public int PlayerScore { get; private set; }
    public int PlayerBestScore { get; private set; }
    public int BestScore { get; private set; }
   
    public string BestPlayerName { get; private set; }
    private SaveData saveData;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        saveFilePath = Application.persistentDataPath + "/savefile.dat";
        Instance = this;        
        PlayerScore = 0;
        PlayerBestScore = 0;
        BestScore = 0;
        BestPlayerName = "???";
        LoadSettings();        
        DontDestroyOnLoad(gameObject);
    }

    public void SaveSettings(string userName, int score)
    {
        PlayerName = userName;
        SaveUserScore(score);        

        using var stream = new FileStream(saveFilePath, FileMode.OpenOrCreate);

        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, saveData);        
    }

    private void SaveUserScore(int score)
    {
        string loweredName = PlayerName.ToLower();
        if (!saveData.ScorePerUser.TryGetValue(loweredName, out int playerScore))
        {
            playerScore = score;
            saveData.ScorePerUser.Add(loweredName, score);
        }
        if (score > playerScore)
        {
            saveData.ScorePerUser[loweredName] = score;            
        }
        PlayerBestScore = saveData.ScorePerUser[loweredName];
        RefreshBestPlayer();
    }

    public void LoadSettings()
    {
        if (!File.Exists(saveFilePath))
        {
            saveData = new SaveData();
            return;
        }
                
        using var stream = new FileStream(saveFilePath, FileMode.Open);

        var formatter = new BinaryFormatter();
        saveData = (SaveData)formatter.Deserialize(stream);
        RefreshBestPlayer();
    }

    private void RefreshBestPlayer()
    {
        var bestPlayer = saveData.ScorePerUser.OrderByDescending(p => p.Value).ToList();
        if (bestPlayer.Count > 0)
        {
            
            
            BestPlayerName = char.ToUpper(bestPlayer[0].Key[0]) + bestPlayer[0].Key.Substring(1);            
            BestScore = bestPlayer[0].Value;
        }
    }
    
}


[System.Serializable]
public class SaveData
{
    public Dictionary<string, int> ScorePerUser { get; set; } = new Dictionary<string, int>();
}
