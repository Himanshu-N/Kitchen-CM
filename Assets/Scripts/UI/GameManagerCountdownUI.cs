using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

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
        countdownText.text = Mathf.Ceil(GameManager.Instance.GameCountdownTimer()).ToString();
        //countdownText.text = GameManager.Instance.GameCountdownTimer().ToString(#.##); shows upto 2 decimal place
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
