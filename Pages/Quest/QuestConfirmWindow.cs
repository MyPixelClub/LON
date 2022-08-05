using Collection;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace FarmPage.Quest
{
    public class QuestConfirmWindow : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private int _requiredAmountEnergy;
        [SerializeField] private GameObject _exeptionBaner;
        [SerializeField] private TMP_Text _exeptionBanerText;
        [SerializeField] private QuestFight _questFight;

        [SerializeField] private AttackDeck _attackDeck;

        public void StartQuest(Chapter chapter)
        {
            if (_requiredAmountEnergy > _player.Energy.CurrentEnergy)
            {
                OpenExceptionBanner("Not enough energy");
                return;
            }

            if (CheckForDeckEmpty() == false)
                return;

            _player.DecreaseEnergy(_requiredAmountEnergy);
            _questFight.StartFight(chapter);
        }

        private bool CheckForDeckEmpty()
        {
            foreach (var card in _attackDeck.CardsInDeck)
            {
                if (card.IsSet)
                    return true;
            }

            OpenExceptionBanner("Not card in deck");
            return false;
        }

        private void OpenExceptionBanner(string exceptionName)
        {
            _exeptionBaner.SetActive(true);
            _exeptionBanerText.text = exceptionName;
        }
    }
}
