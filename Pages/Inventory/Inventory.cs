using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryConfirmWindow _confirmWindow;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerStatisticQuest _playerStatisticQuest;

    [SerializeField] private Transform _container;
    [SerializeField] private InventoryCell _inventoryItemCellTemplate;

    private List<InventoryCell> _itemCollection = new List<InventoryCell>();

    public InventoryConfirmWindow ConfirmWindow => _confirmWindow;
    public List<InventoryCell> ItemCollection => _itemCollection;

    public Player Player => _player;
    public PlayerStatisticQuest PlayerStatisticQuest => _playerStatisticQuest;

    public void AddItem(ShopInventoryItem bottle)
    {
        System.Func<ShopItem, bool> itemContainsList = (a) =>
        {
            foreach (var item in _itemCollection)
            {
                if (item.InventoryItem.name == a.name)
                {
                    item.AmountThisItem++;
                    return true;
                }
            };

            return false;
        };

        if (itemContainsList(bottle) == false)
        {
            var cell = Instantiate(_inventoryItemCellTemplate, _container);
            cell.Render(bottle, this);
            _itemCollection.Add(cell);
        }
    }

    public void ReduceAmount(ShopInventoryItem inventoryItem, int amountValue)
    {
        if (amountValue <= 0 || amountValue > _itemCollection.Count) throw new System.ArgumentOutOfRangeException();


        InventoryCell item = null;

        foreach (var itemInCollection in _itemCollection)
        {
            if (itemInCollection.InventoryItem.GetType() == inventoryItem.GetType())
            {
                item = itemInCollection;
                break;
            }

            throw new System.InvalidOperationException();
        }

        for (int i = 0; i < amountValue; i++)
        {
            item.AmountThisItem--;

            if (item.AmountThisItem == 0)
                Destroy(item.gameObject);

            _itemCollection.Remove(item);
        }
    }
}
