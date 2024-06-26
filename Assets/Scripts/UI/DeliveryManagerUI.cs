using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;


    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManger_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManger_OnRecipeDelivered;

        UpdateVisual();
    }

    private void DeliveryManger_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManger_OnRecipeDelivered(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipeTemplate) continue; 
            Destroy(child.gameObject);
        }

        foreach (RecipeSO waitingRecipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive (true);
            recipeTransform.gameObject.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(waitingRecipeSO);

        }
    }
}
