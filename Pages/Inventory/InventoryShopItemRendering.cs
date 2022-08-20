using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryShopItemRendering : InventoryCategoryRendering
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InvetoryItemType _currentItemCategory;

    private void OnEnable()
    {
        Render();
        GetComponent<Button>().onClick.AddListener(Render);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(Render);
    }

    private void Render()
    {
        _inventory.ConfirmWindow.gameObject.SetActive(false);

        foreach (var item in _inventory.ItemCollection)
            item.gameObject.SetActive(false);

        _inventory.ItemCollection.ForEach(item => 
        {
            if(_currentItemCategory == item.InventoryItem.invetoryItemType)
                item.gameObject.SetActive(true);
        });
    }
}   
