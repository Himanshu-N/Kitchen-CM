using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string MUSIC_LEVEL_PREFS = "MusicVolumePref";
    public static MusicManager Instance { get; private set; }
    AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(MUSIC_LEVEL_PREFS, 0.3f);
        audioSource.volume = volume;
    }

    
    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume >= 1.1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(MUSIC_LEVEL_PREFS, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
