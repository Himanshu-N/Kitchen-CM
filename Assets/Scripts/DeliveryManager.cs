using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int recipesSuccesfullAmount;
    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f )
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax )
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                //Debug.Log(waitingRecipeSO.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            // choosing each Waiting Recipe one by one
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Waiting recipe and Plate Recipe - Has the same number of ingredients
                bool recipeMatched = true;
                foreach(KitchenObjectSO waitingRecipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through each KitchenObjectSO in Waiting Recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateRecipeKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through each KitchenObjectSO in Plate Recipe
                        if (waitingRecipeKitchenObjectSO == plateRecipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //Recipe ingredient didn't match
                        recipeMatched = false;
                        break;
                    }
                }
                
                if(recipeMatched)
                {
                    //Debug.Log("Recipe delivered was correct");
                    waitingRecipeSOList.RemoveAt(i);
                    recipesSuccesfullAmount++;
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // NO matches found
        //Debug.Log("Recipe delivered was not correct");
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSucessfulRecipesAmount()
    {
        return recipesSuccesfullAmount;
    }
}
