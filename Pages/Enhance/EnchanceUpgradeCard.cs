using FarmPage.Enhance.Card_Statistic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Enhance
{
    public class EnchanceUpgradeCard : MonoBehaviour
    {    
        [SerializeField] private EnchanceCardCollection _enhanceCardCollection;
        [SerializeField] private Enchance _enhance;

        [SerializeField] private EnhanceCardForUpgradeStatistic _cardStatistic;

        private CardCollectionCell _cardCell;
        public CardCollectionCell CardCell => _cardCell;

        public void SetCardForUpgrade(CardCollectionCell card)
        {
            if (card == null) throw new System.ArgumentNullException();

            _cardCell = card;
            _cardStatistic.Render(this);
        }


        private void OpenCardCollection()
        {
            _enhanceCardCollection.UpgradeCard = this;
        }
    }
}
