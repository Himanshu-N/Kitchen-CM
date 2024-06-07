using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler<OnPauseToggleEventArgs> OnPauseToggle;
    public class OnPauseToggleEventArgs : EventArgs
    {
        public bool isPausedOrNOt;
    }
    private enum State
    {
        waitingToStart,
        countdownToStart,
        gamePlaying,
        gameOver,
    };
    private State state;

    private float countdownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20f;
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.waitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.waitingToStart)
        {
            state = State.countdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePause();
    }

    private void Update()
    {
        switch(state)
        {
            case State.waitingToStart:
                
                break;
            case State.countdownToStart:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer < 0f)
                {
                    state = State.gamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0f)
                { 
                    state = State.gameOver; 
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.gamePlaying; 
    }  

    public bool IsGameCountdownActive()
    {
        return state == State.countdownToStart;
    }

    public float GameCountdownTimer()
    {
        return countdownTimer;
    }

    public bool IsGameOver()
    {
        return state == State.gameOver;
    }
    public float GamePlayingTimerNormalised()
    {
        return gamePlayingTimer/ gamePlayingTimerMax;
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;

        }
        OnPauseToggle?.Invoke(this, new OnPauseToggleEventArgs{ isPausedOrNOt = isPaused });
    }
}
