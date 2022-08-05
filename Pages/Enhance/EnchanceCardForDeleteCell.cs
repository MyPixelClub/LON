using FarmPage.Enhance.Card_Statistic;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Enhance
{
    [RequireComponent(typeof(Button))]
    public class EnchanceCardForDeleteCell : CardCollectionCell
    {
        [SerializeField] private GameObject _selectImage;
        
        private EnchanceCardsForDeleteCollection _enchanceCardForDeleteCollection;
        private CardCollectionCell _cardInCollection;
        private Enchance _enchance;

        private bool _isSelect;

        protected override void OnEnable()
        {
            _button.onClick.AddListener(SwitchSelectionState);
            _selectImage.SetActive(false);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchSelectionState);
        }

        public void Init(EnchanceCardsForDeleteCollection enchanceCardsForDeleteCollection, Enchance enchance, CardCollectionCell cardInCollection)
        {
            _enchanceCardForDeleteCollection = enchanceCardsForDeleteCollection;
            _enchance = enchance;
            _cardInCollection = cardInCollection;
        }
        
       
        private void SwitchSelectionState()
        {
            if (_isSelect)
            {
                _enchanceCardForDeleteCollection.RetrieveCard(_cardInCollection);
            }
            else
            {
                if (_enchance.UpgradeCard.CardCell.Level + _enchanceCardForDeleteCollection.PossibleLevelUpSlider.HowMuchIncreaseLevel < 25)
                {
                    _enchanceCardForDeleteCollection.AddToDeleteCollection(_cardInCollection);
                    //_cardStatistic.Render(_cardInCollection);
                }
                else
                {
                    return;
                }
            }

            _isSelect = !_isSelect;
            _selectImage.SetActive(_isSelect);
        }
    }
}
