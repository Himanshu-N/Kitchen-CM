using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float burnWarningSoundTimer;
    private float burnWarningSoundTimerMax = 0.2f;
    private bool playBurningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressBarChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playBurningSound = stoveCounter.IsFried() && e.progressNormalised >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playState = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playState)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void Update()
    {
        if (playBurningSound)
        {
            burnWarningSoundTimer -= Time.deltaTime;
            if (burnWarningSoundTimer < 0)
            {
                burnWarningSoundTimer = burnWarningSoundTimerMax;
                SoundManager.Instance.PlayBurningWarnSound(stoveCounter.transform.position);
            }
        }
    }
}
