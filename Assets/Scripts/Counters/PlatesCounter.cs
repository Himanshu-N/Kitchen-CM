using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO platesKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int spawnPlateAmount;
    private int spawnPlateAmountMax = 4;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax )
        {
            //Spawn a plate
            spawnPlateTimer = 0f;
            if (spawnPlateAmount < spawnPlateAmountMax)
            {
                spawnPlateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            //Player is empty handed
            if (spawnPlateAmount > 0)
            {
                //player has atleast one plate
                spawnPlateAmount--;
                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
