using Data;
using FarmPage.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IIncreaserWalletValueAndCardsCount
{
    [SerializeField] private ShopCategoryRendering _startCategory;

    [SerializeField] private ShopCategoryRendering _chestButton, _goldButton, _gemButton;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private CristalWallet _cristalWallet;
    [SerializeField] private CardCollection _cardCollection;
    private ConfirmWindow _confirmWindow;

    public CardCollection CardCollection => _cardCollection;
    public Inventory Inventory => _inventory;
    public GoldWallet GoldWallet => throw new System.NotImplementedException();
    public CristalWallet CristalWallet => _cristalWallet;
    public ConfirmWindow ConfirmWindow => _confirmWindow;

    private void Start()
    {
        _startCategory.SelectCategore();
    }

    private void OnEnable()
    {
        _chestButton.GetComponent<Button>().onClick.AddListener(() => SelectCategory(_chestButton));
        _goldButton.GetComponent<Button>().onClick.AddListener(() => SelectCategory(_goldButton));
        _gemButton.GetComponent<Button>().onClick.AddListener(() => SelectCategory(_gemButton));
    }

    private void OnDisable()
    {
        _chestButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _goldButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _gemButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void SelectCategory(ShopCategoryRendering selectButton)
    {
        _chestButton.CategorySelection.UnselectCategory();
        _gemButton.CategorySelection.UnselectCategory();
        _goldButton.CategorySelection.UnselectCategory();

        selectButton.SelectCategore();
    }

    public void BuyItem(IShopItem shopItem, ConfirmWindow confirmWindow)
    {
        _confirmWindow = confirmWindow;

        shopItem.Buy(this);
    }
}