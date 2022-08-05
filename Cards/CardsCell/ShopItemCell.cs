using Infrastructure.Services;
using FarmPage.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Lumin;
using UnityEngine.UI;

public class ShopItemCell : MonoBehaviour
{
    [SerializeField] private Image _icon;    
    
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Button _buyButton;

    private ConfirmWindow _confirmWindow;

    private ShopItem _shopItem;
    private int _price;

    public int Price => _price;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OpenConfirmWindow);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();
    }

    public void Init(ConfirmWindow confirmWindow)
    {
        _confirmWindow = confirmWindow;
    }
    
    public void Render(IShopItem item)
    {
        _nameText.text = item.Name;
        _icon.sprite = item.UIIcon;
        _price = item.Price;
        _priceText.text = item.PriceText;
        _shopItem = item.Item;
    }

    public void OpenConfirmWindow()
    {
        _confirmWindow.Render(_shopItem);
        _confirmWindow.gameObject.SetActive(true);
    }
}