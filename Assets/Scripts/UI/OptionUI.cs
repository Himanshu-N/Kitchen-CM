using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button backButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI soundEffectText;

    private void Awake()
    {
        Instance = this;

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        backButton.onClick.AddListener(() =>
        {
            Hide();
        });

    }

    private void Start()
    {
        GameManager.Instance.OnPauseToggle += GameManager_OnPauseToggle;
        Hide();
        UpdateVisual();
    }

    private void GameManager_OnPauseToggle(object sender, GameManager.OnPauseToggleEventArgs e)
    {
        if (!e.isPausedOrNOt)
        {
            Hide();
        }
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.Round((SoundManager.Instance.GetVolume()) * 10f);
        musicText.text = "Music: " + Mathf.Round((MusicManager.Instance.GetVolume()) * 10f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
