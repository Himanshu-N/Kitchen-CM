using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;


    public static event EventHandler OnAnyCut;
    public event EventHandler OnCut;

    public event EventHandler<IHasProgress.OnProgressBarChangedEventArgs> OnProgressChanged;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //Clear counter has no kitchen object on top
            if (player.HasKitchenObject() && HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                //Player is carrying something that can be cut/has cutting recipe
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;

                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs { 
                    progressNormalised = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax 
                });

            }
            else
            {
                //Player is not carrying anything
            }
        }
        else
        {
            //Clear Counter has kitchen object on top
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is carrying a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    else
                    {
                        //plate already has that kitchen object
                    }
                }
            }
            else
            {
                //Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs
                {
                    progressNormalised = 0f
                });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeForInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //Clear counter has kitchen object on top and can be cut
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressBarChangedEventArgs 
            { 
                progressNormalised = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax 
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }


    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null){
            return cuttingRecipeSO.output;
        } else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
