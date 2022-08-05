using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Prize 
{
    [SerializeField] private ScriptableObject _prizeAsScriptableObject;

    public virtual IPrize PrizeAsInterface => _prizeAsScriptableObject as IPrize;

    [SerializeField] protected int _minPrizeValue;
    public virtual int AmountPrize => _minPrizeValue;
    public Sprite UIIcon => 
        PrizeAsInterface.UIIcon;
  
    public void TakeItem(IIncreaserWalletValueAndCardsCount increaser)
    {
        PrizeAsInterface.TakeItemAsPrize(increaser, AmountPrize);
    }
}
