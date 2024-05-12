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
            if (player.HasKitchenObject()) {
                //Player is carrying something
            } else {
                //Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}