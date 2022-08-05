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
    [SerializeField] private CardCollection _cardCollection;
    [SerializeField] private EvolveCardCollection _evolveCardCollectionInCollectionPage, _evolutionCardCollectionInEvolvePage;

    [SerializeField] private EvolutionCard _firstCardForEvolution, _secondeCardForEvolution;
    [SerializeField] private Transform _cardCollectionContent;

    [SerializeField] private Button _evolveButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private GameObject _exeptionWindow;

    [SerializeField] private GameObject _evolvedCardWindow;
    [SerializeField] private Image _evolvedCardImage;

    public event UnityAction OnEvolvedCard;
    public EvolutionCard FirstCard => _firstCardForEvolution;
    public EvolutionCard SecondeCard => _secondeCardForEvolution;

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
        }
    }

    private void EvolveCard()
    {
        if (_firstCardForEvolution.IsSet && _secondeCardForEvolution.IsSet)
        {
            _cardCollection.AddCardCell(GetEvolvedCard());
            _cardCollection.DeleteCards(new[] { FirstCard.CardCell, SecondeCard.CardCell });
            OnEvolvedCard?.Invoke();
        }
        else
        {
            _exeptionWindow.SetActive(true);
        }
    }

    private CardCell GetEvolvedCard()
    {
        CardCell evolvedCard = Instantiate(FirstCard.CardCell, _cardCollectionContent);

        evolvedCard.Evolve(_firstCardForEvolution, _secondeCardForEvolution);

        // _evolvedCardWindow.SetActive(true);
        //a_evolvedCardImage.sprite = evolvedCard.Icon.sprite;

        return evolvedCard;
    }    
}

