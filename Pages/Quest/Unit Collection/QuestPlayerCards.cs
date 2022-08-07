using Collection;
using FarmPage.Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayerCards : QuestCollection
{
    public event Action OnValueChanged;

    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private PlayerStatisticQuest _playerStatisticQuest;

    protected override Unit[] GetArrayType()
    {
        int cardsCount = 0;

        foreach (var card in _attackDeck.CardsInDeck)
        {
            if (card.IsSet)
                cardsCount++;
        }

        return new FarmPage.Quest.Card[cardsCount];
    }

    protected override void InitUnit(Unit unit, int position)
    {
        (unit as FarmPage.Quest.Card).Init(_attackDeck.CardsInDeck[position], _playerStatisticQuest);
    }
}
