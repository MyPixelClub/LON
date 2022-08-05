using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Enhance
{
    public class EnchanceCardCell : CardCollectionCell
    {
        [SerializeField] private Button _selectButton;

        private EnchanceCardCollection _enchanceCardCollection;
        private CardCollectionCell _cardInCollection;

        private new void OnEnable()
        {
            _button.onClick.AddListener(() => _statisticWindow.Render(this));
            _selectButton.onClick.AddListener(SelectCard);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveAllListeners();
        }

        public void Init(EnchanceCardCollection enchanceCardCollection, CardCollectionCell cardInCollection)
        {
            _enchanceCardCollection = enchanceCardCollection;
            _cardInCollection = cardInCollection;
        }

        protected virtual void SelectCard()
        {
            _enchanceCardCollection.SelectCard(_cardInCollection);
        }
    }
}
