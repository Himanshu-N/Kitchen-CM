using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningProgressBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private const string IS_BURNING_PROGRESSBAR_FLASH = "IsFlashing";
    bool isFlashing;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_BURNING_PROGRESSBAR_FLASH, false);

    }


    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressBarChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        isFlashing = stoveCounter.IsFried() && e.progressNormalised >= burnShowProgressAmount;

        animator.SetBool(IS_BURNING_PROGRESSBAR_FLASH, isFlashing);
    }
}
