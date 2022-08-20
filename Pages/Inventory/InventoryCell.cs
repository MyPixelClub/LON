using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountThisItemText;

    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _selectionFrame;

    private ShopInventoryItem _inventoryItem;
    private InventoryConfirmWindow _confirmWindow;

    private int _amountThisItem = 1;

    public ShopInventoryItem InventoryItem => _inventoryItem;
    public Sprite Icon => _icon.sprite;

    public int AmountThisItem
    {
        get => _amountThisItem;
        set
        {
            _amountThisItem = value;
            _amountThisItemText.text = _amountThisItem.ToString();
        }
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(SelectItem);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(SelectItem);
    }

    public void Render(ShopInventoryItem shopItem, Inventory inventory)
    {
        _icon.sprite = shopItem.UIIcon;
        _inventoryItem = shopItem;
        _confirmWindow = inventory.ConfirmWindow;
        _selectionFrame.SetActive(false);
    }

    private void SelectItem()
    {
        _confirmWindow.Open(this);
        _selectionFrame.SetActive(true);
    }
}
