using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmPage
{
    public class PrizeWindow : MonoBehaviour, IIncreaserWalletValueAndCardsCount
    {
        [SerializeField] private Transform _container;
        [SerializeField] private PrizeCell _prizeCellTamplate;

        [SerializeField] private CristalWallet _cristalWallet;
        [SerializeField] private GoldWallet _goldWallet;
        [SerializeField] private Inventory _inventory;

        public CardCollection CardCollection => throw new System.NotImplementedException();

        public CristalWallet CristalWallet => _cristalWallet;

        public GoldWallet GoldWallet => _goldWallet;

        public Inventory Inventory => _inventory;

        private void OnEnable()
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        public void Render(Prize[] prizes)
        {
            gameObject.SetActive(true);

            foreach (var prize in prizes)
            {
                var cell = Instantiate(_prizeCellTamplate, _container);
                cell.RenderGetingPrize(prize);

                cell.Prize.TakeItemAsPrize(this, cell.AmountPrize);
            }
        }
    }
}
