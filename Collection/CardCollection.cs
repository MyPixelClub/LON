using System;
using System.Collections.Generic;
using System.Linq;
using Collection;
using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;


public class CardCollection : CardCollectionSort<CardCollectionCell>
{
    [SerializeField] private StatisticWindow _statisticWindow;
    [SerializeField] private CardCollectionCell _cardCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Card[] _variationCards;

    public List<CardCollectionCell> Cards => _cards;

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            AddCard(_variationCards[UnityEngine.Random.Range(0, _variationCards.Length)]);
        }
    }
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        ActiveAllCards();

        _cards = _cards.OrderByDescending(e => e.Power).ThenByDescending(e => e.Card.Rarity).ToList();
        RenderCardsSiblingIndex();
    }

    private void ActiveAllCards()
    {
        foreach (var item in _cards) 
            item.gameObject.SetActive(true);
    }

    public void AddCards(Card[] newCards)
    {
        foreach (var newCard in newCards)
            AddCard(newCard);
    }

    public void AddCard(Card newCard)
    {
        newCard.Id = 1;

        var cell = Instantiate(_cardCellTemplate, _container);
        cell.Render(newCard);
        _cards.Add(cell);
        cell.InitStatisticCard(_statisticWindow);
    }

    public void AddCardCell(CardCell cardCell)
    {
        if (cardCell == null) throw new ArgumentNullException();

        var newCell = Instantiate(_cardCellTemplate, _container);
        newCell.Render(cardCell);
        _cards.Add(newCell);
        newCell.InitStatisticCard(_statisticWindow);
    }

    public void DeleteCards(CardCollectionCell[] cardsForDelete)
    {
        foreach (var card in cardsForDelete)
        {
            Destroy(card.gameObject);
            _cards.Remove(card);
        }
    }
}

