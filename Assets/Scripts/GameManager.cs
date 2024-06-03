using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    private enum State
    {
        waitingToStart,
        countdownToStart,
        gamePlaying,
        gameOver,
    };
    private State state;

    private float waitingTimer = 1f;
    private float countdownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 15f;

    private void Awake()
    {
        Instance = this;
        state = State.waitingToStart;
    }

    private void Update()
    {
        switch(state)
        {
            case State.waitingToStart:
                waitingTimer -= Time.deltaTime;
                if (waitingTimer < 0f)
                {   
                    state = State.countdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
        Debug.Log(state);
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

}