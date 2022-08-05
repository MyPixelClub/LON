using System.Collections.Generic;
using Infrastructure.Services;
using FarmPage.Enhance.Card_Statistic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Enhance
{
    public class Enchance : MonoBehaviour
    {
        [SerializeField] private CardCollection _cardCollection;
        [SerializeField] private EnchanceCardCollection _enhanceCardForUpgradeCollection;
        [SerializeField] private EnchanceCardsForDeleteCollection _enhanceCardsForDeleteCollection;

        [SerializeField] private EnhanceCardForUpgradeStatistic _upgradeCardStatistic;
        [SerializeField] private EnchanceUpgradeCard _upgradeCard;

        [SerializeField] private Button _enhanceButton;
        [SerializeField] private GameObject _exeptionWindow;
        [SerializeField] private PossibleLevelUpSlider _possibleLevelUpSlider;

        public EnchanceUpgradeCard UpgradeCard => _upgradeCard;

        private void OnEnable()
        {
            _enhanceButton.onClick.AddListener(Enhance);
        }

        private void OnDisable()
        {
            _enhanceButton.onClick.RemoveListener(Enhance);
        }

        public void SetCardForUpgrade(CardCollectionCell cardCollectionCell)
        {
            _upgradeCard.SetCardForUpgrade(cardCollectionCell);
            gameObject.SetActive(true);
        }

        private void Enhance()
        {
            List<CardCollectionCell> currentEnhanceCardList = new();

            if (_enhanceCardsForDeleteCollection.CardForDelete.Count == 0)
            {
                _exeptionWindow.SetActive(true);
                return;
            }

            _possibleLevelUpSlider.Reset();

            if (_upgradeCard.CardCell == null) return;

            _upgradeCard.CardCell.LevelUp(_enhanceCardsForDeleteCollection.CardForDelete.ToArray());
            _upgradeCardStatistic.Render(_upgradeCard);
            _cardCollection.DeleteCards(_enhanceCardsForDeleteCollection.CardForDelete.ToArray());

            currentEnhanceCardList.AddRange(_cardCollection.Cards);
            currentEnhanceCardList.Remove(_upgradeCard.CardCell);

            _enhanceCardsForDeleteCollection.DisplayCardsForDelete(currentEnhanceCardList);
        }
    }
}
