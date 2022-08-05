using System;
using System.Collections.Generic;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class CardCellInDeck : CardCell
{
    [SerializeField] private GameObject _emptyFone, _avatar;
    [SerializeField] private Deck _deck;
    [SerializeField] private StatisticWindow _statisticWindow;

    [SerializeField] private Button _button;

    public GameObject EmptyFone => _emptyFone;
    public GameObject Avatar => _avatar;

    private bool _isSet;
    public bool IsSet => _isSet;

    public UnityAction ResetFromDeck => ResetComponent;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => _statisticWindow.Render(this));
        _button.interactable = _isSet;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void SetCard(CardCollectionCell cardCell)
    {
        Render(cardCell);
        _isSet = true;
        _button.interactable = true;
    }

    private void ResetComponent()
    {
        _deck.RetrieveCardInCollection(this, transform.GetSiblingIndex());
        _isSet = false;
        _button.interactable = false;
    }
}

