using System;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private int _price;

    [SerializeField] private int _count;
    [SerializeField] private string _purchaseText;
    [SerializeField] private string _name;
    
    public Sprite UIIcon => _image;
    public int Price => _price;
    public int Count => _count;
    public ShopItem Item => this;
    public virtual string PriceText => $"{Price} Gold";
    public string PurchaseText => _purchaseText;
    public string Name => _name;
}


