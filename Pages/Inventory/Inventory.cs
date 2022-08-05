using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryConfirmWindow _confirmWindow;
    private List<ShopItemBottle> _bottleCollection = new List<ShopItemBottle>();

    public InventoryConfirmWindow ConfirmWindow => _confirmWindow;
    public List<ShopItemBottle> BottleCollection => _bottleCollection;

    public void AddItem(ShopItemBottle bottle)
    {
        _bottleCollection.Add(bottle);
    }

    public void UseEnergyBottle(InventoryCell item)
    {
        DestroyItem(item);
    }

    private void DestroyItem(InventoryCell bottel)
    {
        bottel.AmountThisItem--;

        if (bottel.AmountThisItem == 0)
            Destroy(bottel.gameObject);

        _bottleCollection.Remove(bottel.Bottel);
    }
}
