using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] Image icon;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        icon.sprite = kitchenObjectSO.sprite;
    }
}
