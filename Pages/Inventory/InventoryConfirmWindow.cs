using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryConfirmWindow : MonoBehaviour
{
    [SerializeField] private Button _useButton;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Image _itemImage;

    private InventoryCell _inventoryCell;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(UseEffect);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveAllListeners();
    }

    public void Open(InventoryCell inventoryItem)
    {
        _useButton.interactable = inventoryItem.InventoryItem.IsUseableInInventory;

        gameObject.SetActive(true);
        _inventoryCell = inventoryItem;

        _itemImage.sprite = inventoryItem.Icon;
    }

    private void UseEffect()
    {
        _inventoryCell.InventoryItem.UseEffect(_inventory);
        gameObject.SetActive(false);
    }
}
