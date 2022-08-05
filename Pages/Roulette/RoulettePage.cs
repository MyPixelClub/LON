using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class RoulettePage : MonoBehaviour, IIncreaserWalletValueAndCardsCount
{
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private GoldWallet _goldWallet;
    [SerializeField] private Inventory _inventory;

    public CardCollection CardCollection => _cardCollection;

    public CristalWallet CristalWallet => _cristalWallet;

    public GoldWallet GoldWallet => _goldWallet;

    public Inventory Inventory => _inventory;

    public void TakeItem(Prize rouletteItem)
    {
       rouletteItem.TakeItem(this);
    }    
}

