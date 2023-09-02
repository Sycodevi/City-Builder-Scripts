using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveGameSystem : MonoBehaviour
{
    public UIController uiController;
    public ResourceManager resourceManager;

    public TextMeshProUGUI likesValue;
    public TextMeshProUGUI populationValue;
    public TextMeshProUGUI moneyValue;

    private string saveFilePath;

    private void Awake()
    {
        // Set the save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    private void Start()
    {
        if (LoadManager.Instance.isGameLoaded == true)
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        saveData.scene = SceneManager.GetActiveScene().name;
        saveData.level = uiController.Level;
        saveData.money = int.Parse(moneyValue.text);
        saveData.population = int.Parse(populationValue.text);
        saveData.likes = int.Parse(likesValue.text);

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Save file path: " + saveFilePath);

        Debug.Log("Game saved.");
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            //uiController.Level = saveData.level;
            //moneyHelper = new MoneyHelper(saveData.money);
            //populationScript.population = saveData.population;
            //likesScript.taxedLikes = saveData.likes;

            uiController.SetLevel(saveData.level);
            resourceManager.SetMoney(saveData.money);
            resourceManager.SetPopulation(saveData.population);
            resourceManager.SetLikes(saveData.likes);

            Debug.Log("Game loaded from : " + saveFilePath);
            LoadManager.Instance.isGameLoaded = false;
        }
        else
        {
            Debug.Log("No saved game found.");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string scene;
    public int level;
    public int money;
    public int population;
    public int likes;
}