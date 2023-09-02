using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    SaveGameSystem saveGameSystem;
    public AudioScript AudioScriptGameObj;

    public Button NewGameBtn;
    public Button LoadGameBtn;
    public Button OptionsBtn;
    public Button AboutBtn;
    public Button ExitBtn;
    public Button Grass, Desert, Snow;
    public Button Minus, Plus;

    public GameObject NewGamePanel, OptionsPanel, AboutPanel;


    public TextMeshProUGUI soundValueText;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
    }


    private void Start()
    {
        NewGameBtn.onClick.AddListener(NewGameHandler);
        LoadGameBtn.onClick.AddListener(LoadGameHandler);
        Grass.onClick.AddListener(LoadGrassLand);
        Desert.onClick.AddListener(LoadDesertLand);
        Snow.onClick.AddListener(LoadSnowland);
        OptionsBtn.onClick.AddListener(OptionsHandler);
        Minus.onClick.AddListener(onClickMinusSoundButton);
        Plus.onClick.AddListener (onClickPlusSoundButton);
        AboutBtn.onClick.AddListener(AboutHandler);
        ExitBtn.onClick.AddListener(ExitHandler);
    }

    private void ExitHandler()
    {
        Application.Quit();
    }

    private void AboutHandler()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        if (AboutPanel.activeSelf == true)
        {
            AboutPanel.SetActive(false);
        }
        else
        {
            NewGamePanel.SetActive(false);
            OptionsPanel.SetActive(false);
            AboutPanel.SetActive(true);
        }
    }

    private void OptionsHandler()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        if (OptionsPanel.activeSelf == true)
        {
            OptionsPanel.SetActive(false);
        }
        else
        {
            NewGamePanel.SetActive(false);
            AboutPanel.SetActive(false);
            OptionsPanel.SetActive(true);
        }
    }

    private void onClickMinusSoundButton()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        //AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume -= .10f;
        if (AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume == 0f)
        {
            return;
        }
        else
        {
            AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume -= 0.10f;
            soundValueText.text = (Mathf.RoundToInt(AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume * 100) + "%");
        }
    }

    public void onClickPlusSoundButton()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        if (AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume == 1f)
        {
            return;
        }
        else
        {
            AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume += 0.10f;
            soundValueText.text = (Mathf.RoundToInt(AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume * 100) + "%");
        }
    }
    private void LoadSnowland()
    {
        SceneManager.LoadScene("Snowland");
    }

    private void LoadDesertLand()
    {
        SceneManager.LoadScene("Desert");
    }

    private void LoadGrassLand()
    {
        SceneManager.LoadScene("Grassland");
    }

    private void NewGameHandler()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        if(NewGamePanel.activeSelf == true)
        {
            NewGamePanel.SetActive(false);
        }
        else
        {
            AboutPanel.SetActive(false);
            OptionsPanel.SetActive(false);
            NewGamePanel.SetActive(true);
        }
    }
    private void LoadGameHandler()
    {
        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        if (File.Exists(saveFilePath))
        {
            LoadManager.Instance.isGameLoaded = true;
            SceneManager.LoadScene(saveData.scene);
        }
        else
        {
            Debug.Log("No data " + saveData.scene);
        }
    }
}
