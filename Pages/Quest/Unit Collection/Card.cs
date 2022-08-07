using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmPage.Quest
{

    public class Card : Unit
    {
        private PlayerStatisticQuest _player;

        private CardCell _card;        

        public override int Damage()
        {
            return _card.Attack;
        }

        public void Init(CardCell card, PlayerStatisticQuest player)
        {
            _card = card;
            _health = card.Health;
            _maxHealth = card.Health;
            _view.sprite = _card.Card.SquereUIICon;
            _player = player;
            Init();
        }

        protected override void DecreaseHealth(float amountDamage)
        {
            _health -= amountDamage * 0.7f;
            _player.TakeDamage(amountDamage * 0.3f);
        }
    }
}
