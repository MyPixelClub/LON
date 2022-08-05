using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmPage.Quest
{

    public class Card : Unit
    {
        private CardCell _card;

        public override int Damage()
        {
            return _card.Attack;
        }

        public void Init(CardCell card)
        {
            _card = card;
            _health = card.Health;
            _maxHealth = card.Health;
            _view.sprite = _card.Card.UIIcon;
            Init();
        }
    }
}
