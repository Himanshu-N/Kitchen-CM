using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingTimerUI : MonoBehaviour
{
    [SerializeField] private Image playingTimer;


    private void Update()
    {
        playingTimer.fillAmount = GameManager.Instance.GamePlayingTimerNormalised();
    }

}
