using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gold", menuName = "ScriptableObjects/Shop/Gold")]
public class ShopItemGold : ScriptableObject, IPrize
{
    [SerializeField] private Sprite _icon;

    public Sprite UIIcon => _icon;

    public void TakeItemAsPrize(IIncreaserWalletValueAndCardsCount increaser, int amountValue)
    {
        increaser.GoldWallet.Add—urrency(amountValue);
    }
}
