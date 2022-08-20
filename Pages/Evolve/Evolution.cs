using Data;
using Infrastructure.Services;
using FarmPage.Evolve;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;

public class Evolution : MonoBehaviour
{
    public event UnityAction OnEvolvedCard;

    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EvolveCardCollection _evolveCardCollectionInCollectionPage, _evolutionCardCollectionInEvolvePage;

    [SerializeField] private EvolutionCard _firstCardForEvolution, _secondeCardForEvolution;
    [SerializeField] private Transform _cardCollectionContent;

    [SerializeField] private Button _evolveButton;
    [SerializeField] private Button _backButton;

    public EvolutionCard FirstCard => _firstCardForEvolution;
    public EvolutionCard SecondeCard => _secondeCardForEvolution;

    public Sprite EvolvedCardSprite { get; private set; }

    private void OnEnable()
    {
        _evolveCardCollectionInCollectionPage.SetCardCollection(_cardCollection.Cards);

        _backButton.onClick.AddListener(() => 
        {
            Reset();
            gameObject.SetActive(false);
        });
        _evolveButton.onClick.AddListener(EvolveCard);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
        _evolveButton.onClick.RemoveListener(EvolveCard);
        _evolveButton.interactable = false;
    }

    public void Reset()
    {
        _firstCardForEvolution.Reset();
        _secondeCardForEvolution.Reset();

        _evolveCardCollectionInCollectionPage.SetCardCollection(_cardCollection.Cards);
        _evolutionCardCollectionInEvolvePage.gameObject.SetActive(false);
        _evolutionCardCollectionInEvolvePage.gameObject.SetActive(true);
    }

    public void SelectCard(CardCollectionCell cardCollectionCell, List<CardCollectionCell> cardsForEvolve)
    {
        if (_firstCardForEvolution.CardCell == null)
        {
            _firstCardForEvolution.SetCard(cardCollectionCell);
            _evolutionCardCollectionInEvolvePage.SetCardCollection(cardsForEvolve);
            gameObject.SetActive(true);
        }
        else
        {
            _secondeCardForEvolution.SetCard(cardCollectionCell);
            _evolutionCardCollectionInEvolvePage.SetCardCollection(cardsForEvolve);
            _evolveButton.interactable = true;
        }
    }

    private void EvolveCard()
    {
        if (_firstCardForEvolution.IsSet == false || _secondeCardForEvolution.IsSet == false)
            throw new System.InvalidOperationException();

        _cardCollection.AddCardCell(GetEvolvedCard());
        _cardCollection.DeleteCards(new[] { FirstCard.CardCell, SecondeCard.CardCell });
        OnEvolvedCard?.Invoke();
        _evolveButton.interactable = false;
    }

    private CardCell GetEvolvedCard()
    {
        CardCell evolvedCard = Instantiate(FirstCard.CardCell, _cardCollectionContent);

        evolvedCard.Evolve(_firstCardForEvolution, _secondeCardForEvolution);

        EvolvedCardSprite = evolvedCard.UIIcon;

        return evolvedCard;
    }    
}

