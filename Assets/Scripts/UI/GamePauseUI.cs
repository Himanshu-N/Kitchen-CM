using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Transform optionUITransform;

    private void Start()
    {
        GameManager.Instance.OnPauseToggle += GameManager_OnPauseToggle;
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePause();
        });
        optionButton.onClick.AddListener(() =>
        {
            OptionUI.Instance.Show();
        });

        Hide();
    }

    private void GameManager_OnPauseToggle(object sender, GameManager.OnPauseToggleEventArgs e)
    {
        if (e.isPausedOrNOt == true)
        {
            Show();
        }
        else
        {
            Hide();
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
