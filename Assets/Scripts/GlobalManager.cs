using UnityEngine;


public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveSettings()
    {
        //SaveData data = new SaveData();
        //data.TeamColor = TeamColor;

        //string json = JsonUtility.ToJson(data);

        //File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadSettings()
    {
        //string path = Application.persistentDataPath + "/savefile.json";
        //if (File.Exists(path))
        //{
        //    string json = File.ReadAllText(path);
        //    SaveData data = JsonUtility.FromJson<SaveData>(json);

        //    TeamColor = data.TeamColor;
        //}
    }
}
