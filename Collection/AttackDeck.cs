using UnityEngine;

namespace Collection
{
    public class AttackDeck : Deck
    {
        public int Power
        {
            get
            {
                int amountPower = 0;

                foreach (var card in _cardsInDeck)
                    amountPower += card.Power;

                return amountPower;
            }
        }
    }
}

