using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Shop
{
    public class ConfirmWindow : MonoBehaviour 
    {
        [SerializeField] private float _animationTime;

        [SerializeField] private global::Shop _shop;
        [SerializeField] private TMP_Text _amountItemText;
               
        [SerializeField] private GoldWallet _goldWallet;

        [SerializeField] private Animator _openChestAnimation;
        [SerializeField] private SpriteRenderer _animationItemImage;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _buyButton;

        [SerializeField] private ShopCardTaakenWindow _cardsRepresentation;

        [SerializeField] private ParticleSystem _doneBuyEffect;

        public Action CardRepresentation(Card[] cards) => () => StartCoroutine(AnimationOpenChest(cards));

        private string _chestAnimationCommand;
 
        private IShopItem _shopItem;

        public void OnEnable()
        {
            _openChestAnimation.enabled = true;
        }

        private void OnDisable()
        {
            _openChestAnimation.enabled = false;
            _doneBuyEffect.gameObject.SetActive(false);
        }
        public void Render(ShopItem item)
        {
            _buyButton.interactable = true;
            _amountItemText.text = $"Purchase ({item.PurchaseText})";

            _shopItem = item as IShopItem;

            _itemImage.gameObject.SetActive(item is ShopItemCardPack == false);
            _animationItemImage.gameObject.SetActive(item is ShopItemCardPack);

            if (item is ShopItemCardPack)
            {
                _chestAnimationCommand = (item as ShopItemCardPack).ChestCommand;
                _animationItemImage.sprite = item.UIIcon;
            }
            else
            {
                _chestAnimationCommand = "";
                _itemImage.sprite = item.UIIcon;
            }
        }

        public void Buy()
        {
            _buyButton.interactable = false;

            _shop.BuyItem(_shopItem, this);
            
            _goldWallet.Withdraw—urrency(_shopItem.Price);
            _openChestAnimation.Play(_chestAnimationCommand);

            if (_shopItem.Item is not ShopItemCardPack)
                _doneBuyEffect.gameObject.SetActive(true);

        }

        private IEnumerator AnimationOpenChest(Card[] cards)
        {
            yield return new WaitForSeconds(_animationTime);

            gameObject.SetActive(false);
            _cardsRepresentation.Render(cards.ToList());
        }
    }
}