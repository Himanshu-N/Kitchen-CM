using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] GameObject hasProgressGameObject;

    private IHasProgress hasProgress;
    // Start is called before the first frame update
    void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("The game object " + hasProgressGameObject + " doesn't have interface IHasProgress");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressBarChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalised;

        if(e.progressNormalised == 0f|| e.progressNormalised ==1f) {
            Hide();
        } else {
            Show();
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
