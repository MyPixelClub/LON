using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RevertHealthWindow : MonoBehaviour
{
    [SerializeField] private Image _revertHealthItemImage;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Button _useItemButton;

    private InventoryCell _revertHealthItemCell;

    private void OnEnable()
    {
        _useItemButton.onClick.AddListener(() => 
        {
            _revertHealthItemCell.InventoryItem.UseEffect(_inventory);
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _useItemButton.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    public IEnumerator Open()
    {
        _revertHealthItemCell = _inventory.ItemCollection.FirstOrDefault(t => t.InventoryItem is ShopItemRevertHelthInQuestBottle);
        if (_revertHealthItemCell == null) yield break;

        gameObject.SetActive(true);

        _revertHealthItemImage.sprite = _revertHealthItemCell.InventoryItem.UIIcon;

        while (gameObject.activeInHierarchy)
        {
            yield return null;
        }
    }
}
