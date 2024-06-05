using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] MusicClipRefsSO musicClipRefsSO;
    private const string SOUNDEFFECTS_LEVEL_PREFS = "PrefSoundLevel";
    public static SoundManager Instance { get; private set; }
    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(SOUNDEFFECTS_LEVEL_PREFS, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        PlaySoundArray(musicClipRefsSO.trash, ((TrashCounter)sender).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySoundArray(musicClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySoundArray(musicClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySoundArray(musicClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySoundArray(musicClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySoundArray(musicClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySoundArray(AudioClip[] audioClip, Vector3 audioClipPos, float volume = 1f)
    {
        PlaySound(audioClip[Random.Range(0,audioClip.Length)], audioClipPos, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 audioClipPos, float volumeMultiplier=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, audioClipPos, volume * volumeMultiplier);

    }

    public void PlayFootstepSound(Vector3 position,  float volume)
    {
        PlaySoundArray(musicClipRefsSO.footstep, position, volume);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume >= 1.1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(SOUNDEFFECTS_LEVEL_PREFS, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
