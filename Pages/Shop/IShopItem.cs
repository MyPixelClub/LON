using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItem 
{
    public Sprite UIIcon { get; }
    public int Price { get; }
    public ShopItem Item { get; }
    public int Count { get; }
    public string PriceText { get; }
    public string Name { get; }

    public void Buy(IIncreaserWalletValueAndCardsCount increaser);
}
