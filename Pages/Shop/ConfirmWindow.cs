using Infrastructure.Services;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Shop
{
    public class ConfirmWindow : MonoBehaviour 
    {
        private const int MaxCountCard = 50;

        [SerializeField] private global::Shop _shop;
        [SerializeField] private TMP_Text _amountItemText;
               
        [SerializeField] private GoldWallet _goldWallet;

        [SerializeField] private Animator _openChestAnimation;
        [SerializeField] private Button _buyButton;

        private string _chestAnimationCommand;
 
        private IShopItem _shopItem;        
    
        public void Render(ShopItem item)
        {
            _buyButton.interactable = true;
            _amountItemText.text = $"Purchase ({item.PurchaseText})";

            _shopItem = item as IShopItem;

            _openChestAnimation.GetComponent<SpriteRenderer>().sprite = item.UIIcon;

            if (item is ShopItemCardPack)
                _chestAnimationCommand = (item as ShopItemCardPack).ChestCommand;
            else
                _chestAnimationCommand = "";
        }

        public void Buy()
        {
            _buyButton.interactable = false;
            _shop.BuyItem(_shopItem);
            
            _goldWallet.Withdraw—urrency(_shopItem.Price);

            _openChestAnimation.Play(_chestAnimationCommand);
        }
    }
}