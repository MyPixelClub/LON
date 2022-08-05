using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BottleEffects
{
    None,
    ReplenishEnergy,
    ReplenishHealth
}

[CreateAssetMenu(fileName = "Bottle", menuName = "ScriptableObjects/Shop/Bottle")]
public class ShopItemBottle : ShopItem, IInventory, IPrize, IShopItem
{
    [SerializeField] private BottleEffects _bottleEffects;

    public BottleEffects Effect => _bottleEffects;
    public int Id;

    public void TakeItemAsPrize(IIncreaserWalletValueAndCardsCount increaser, int amountValue)
    {
        for (int i = 0; i < amountValue; i++)
            increaser.Inventory.AddItem(this);
    }

    public void UseEffect(Inventory inventory, InventoryCell inventoryCell)
    {
        inventory.UseEnergyBottle(inventoryCell);
    }

    public void Buy(IIncreaserWalletValueAndCardsCount increaser)
    {
        increaser.Inventory.AddItem(this);
    }
}
