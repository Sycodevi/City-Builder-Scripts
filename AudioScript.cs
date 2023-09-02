using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private static AudioScript _instance;

    public static AudioScript Instance { get => _instance; }

    public AudioClip buttonClickSFX, PlaceBuildingSFX, RemoveBuildingSFX;
    public AudioSource FXAudioSource;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void PlayButtonClickedSFX()
    {
        FXAudioSource.Stop();
        FXAudioSource.clip = buttonClickSFX;
        FXAudioSource.Play();
    }
    public void RemoveBuildingButtonSFX()
    {
        FXAudioSource.Stop();
        FXAudioSource.clip = RemoveBuildingSFX;
        FXAudioSource.Play();
    }
    public void PlaceBuildingButtonClickedSFX()
    {
        FXAudioSource.Stop();
        FXAudioSource.clip = PlaceBuildingSFX;
        FXAudioSource.Play();
    }
}
