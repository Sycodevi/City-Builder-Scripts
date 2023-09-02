using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissingButtonsAudioScript : MonoBehaviour
{
    public Button Residential;
    public Button Commercial;
    public Button Industrial;
    public Button Service;
    public Button Road;

    private void Start()
    {
        Residential.onClick.AddListener(PlayAudio);
        Commercial.onClick.AddListener(PlayAudio);
        Industrial.onClick.AddListener(PlayAudio);
        Service.onClick.AddListener(PlayAudio);
        Road.onClick.AddListener(PlayAudio);
    }
    void PlayAudio()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
    }
}

