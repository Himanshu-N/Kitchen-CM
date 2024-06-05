using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    Animator animator;
    private int previousCountdownNumber;
    private const string NUMBER_POPUP = "PopUp";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManger_OnStateChanged;

        Hide();
    }

    private void GameManger_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameCountdownActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GameCountdownTimer());
        countdownText.text = countdownNumber.ToString();
        //countdownText.text = GameManager.Instance.GameCountdownTimer().ToString(#.##); shows upto 2 decimal place

        if (countdownNumber != previousCountdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
