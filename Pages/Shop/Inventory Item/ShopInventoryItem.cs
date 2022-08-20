using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvetoryItemType
{
    Item,
    Material,
    Rune
}

public abstract class ShopInventoryItem : ShopItem, IInventoryItem, IPrize, IShopItem
{
    public bool IsUseableInInventory;
    public InvetoryItemType invetoryItemType;

    public void TakeItemAsPrize(IIncreaserWalletValueAndCardsCount increaser, int amountValue)
    {
        for (int i = 0; i < amountValue; i++)
            increaser.Inventory.AddItem(this);
    }

    public void Buy(IIncreaserWalletValueAndCardsCount increaser)
    {
        increaser.Inventory.AddItem(this);
    }

    public abstract void UseEffect(Inventory inventory);
}
