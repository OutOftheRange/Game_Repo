using System.IO;
using UnityEngine;
using System.Reflection;

public class JsonChangeData : MonoBehaviour
{
    private static JsonChangeData instance;
    private UnlockLevels.GameProgress gameProgress; 

    public static JsonChangeData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JsonChangeData>();
            }
            return instance;
        }
    }

    private string jsonPath;

    void Start()
    {
        jsonPath = Path.Combine(Application.streamingAssetsPath, "gameProgress.json");
        LoadGameProgress();
    }

    public void ModifyGameProgress(string levelKey, bool newValue)
    {
        string json = System.IO.File.ReadAllText(jsonPath);
        gameProgress = JsonUtility.FromJson<UnlockLevels.GameProgress>(json);

        System.Reflection.FieldInfo field = typeof(UnlockLevels.GameProgress).GetField(levelKey);
        if (field != null)
        {
            field.SetValue(gameProgress, newValue);
            Debug.Log("Game progress modified for level: " + levelKey + " to value: " + newValue);
        }
        else
        {
            Debug.LogWarning("Unknown level key: " + levelKey);
        }

        SaveGameProgress(gameProgress);
    }

    private void SaveGameProgress(UnlockLevels.GameProgress gameProgress)
    {
        string modifiedJson = JsonUtility.ToJson(gameProgress, true);
        System.IO.File.WriteAllText(jsonPath, modifiedJson);
    }

    private void LoadGameProgress()
    {
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            gameProgress = JsonUtility.FromJson<UnlockLevels.GameProgress>(json);
        }
        else
        {
            gameProgress = new UnlockLevels.GameProgress(
                false, false, false, false, false, false,
                false, false, false, false, false, false,
                false, false); 
            SaveGameProgress(gameProgress);
        }

    }
}
