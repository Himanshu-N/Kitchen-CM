using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] counterVisualArray;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject counterVisual in counterVisualArray)
        {
            counterVisual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject counterVisual in counterVisualArray)
        { 
            counterVisual.SetActive(false);
        }
    }
}
