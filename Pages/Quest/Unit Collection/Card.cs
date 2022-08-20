using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Quest
{

    public class Card : Unit
    {
        [SerializeField] private TMP_Text _levelText;
        private PlayerStatisticQuest _player;

        private CardCell _card;        

        public override int Damage()
        {
            return _card.Attack;
        }

        public void Init(CardCell card, PlayerStatisticQuest player, Image blickImage)
        {
            _card = card;
            _blick = blickImage;
            _health = card.Health;
            _maxHealth = card.Health;
            _view.sprite = _card.UIIcon;
            _levelText.text = _card.Level.ToString();
            _player = player;
            Init();
        }

        protected override void DecreaseHealth(float amountDamage)
        {
            amountDamage = _card.GetDamageValueAfterResist(amountDamage);

            _health -= amountDamage * 0.7f;
            _player.TakeDamage(amountDamage * 0.3f);
        }
    }
}
