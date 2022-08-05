using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Cristal", menuName = "ScriptableObjects/Shop/Cristal")]
public class ShopItemCristal : ShopItem, IShopItem, IPrize
{
    public void Buy(IIncreaserWalletValueAndCardsCount increaser)
    {
        increaser.CristalWallet.Add—urrency(Count);
    }

    public void TakeItemAsPrize(IIncreaserWalletValueAndCardsCount increaser, int amountValue)
    {
        increaser.CristalWallet.Add—urrency(amountValue);
    }
}
