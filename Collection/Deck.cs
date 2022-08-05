using System;
using System.Collections.Generic;
using Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class Deck : MonoBehaviour
{
    public event Action<List<CardCellInDeck>> OnCardChanged;

    [SerializeField] protected List<CardCellInDeck> _cardsInDeck;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private StatisticWindow _statisticWindow;

    private int _cardPositionInDeck = 0;
    
    public List<CardCellInDeck> CardsInDeck => _cardsInDeck;


    private void OnEnable()
    {
        OnCardChanged?.Invoke(_cardsInDeck);
    }

    public void SetCardInDeck(CardCollectionCell cardCell)
    {
        if (cardCell == null) throw new ArgumentNullException();

        _cardPositionInDeck = GetNearbySlot();

        if (_cardPositionInDeck < 0) return;

        _cardsInDeck[_cardPositionInDeck].EmptyFone.SetActive(false);
        _cardsInDeck[_cardPositionInDeck].Avatar.SetActive(true);
        _cardsInDeck[_cardPositionInDeck].SetCard(cardCell);
        _cardCollection.DeleteCards(new[] { cardCell });
        if (_cardPositionInDeck == _cardsInDeck.Count) throw new ArgumentOutOfRangeException();
    }

    public void RetrieveCardInCollection(CardCell card, int cardPosition)
    {
        if (_cardsInDeck[cardPosition].IsSet == false) return;

        _cardsInDeck[cardPosition].EmptyFone.SetActive(true);
        _cardsInDeck[cardPosition].Avatar.SetActive(false);
        _cardCollection.AddCardCell(card);
    }

    private int GetNearbySlot()
    {
        foreach (var card in _cardsInDeck)
        {
            if (card.IsSet == false)
                return card.transform.GetSiblingIndex();
        }

        return -1;
    }
}