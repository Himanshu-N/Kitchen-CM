using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if(!HasKitchenObject()) {
            //Clear counter has no kitchen object on top
            if(player.HasKitchenObject()) {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                //Player is not carrying anything
            }
        } else {
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
                else
                {
                    //player is carrying something that is not plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Clear counter has plate on top
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                        else
                        {
                            //plate already has that kitchen object
                        }
                    }
                }
            } else {
                //Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}