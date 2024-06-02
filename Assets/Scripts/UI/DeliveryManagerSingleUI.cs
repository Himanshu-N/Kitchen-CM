using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeUIText;
    [SerializeField] Transform iconTemplate;
    [SerializeField] Transform iconContainer;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeUIText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTemplateTransform = Instantiate(iconTemplate, iconContainer);
            iconTemplateTransform.gameObject.SetActive(true);
            iconTemplateTransform.gameObject.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
