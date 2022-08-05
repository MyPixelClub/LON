using Cards;
using Data;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public abstract class CardCell : MonoBehaviour, ICard
{
    public event UnityAction OnLevelUp;

    [SerializeField] protected Image _icon;
    [SerializeField] private TMP_Text _cardName;
    
    [SerializeField] private CardStatsPanel _cardStatsPanel;
    
    protected Card _card;

    private int _health;
    private int _attack;
    private int _def;
    private int _level;
    private int _evolution;

    private int _levelPoint;
    private int _maxLevelPoint;
    
    private int _maxLevel = 25;
    
    private float _nextMaxLevelPointMultiplier = 1.1f;
    private int _baseEnhancmentLevelPoint = 1500;

    private const float ValueIncreaseMultiplier = 1.35f;
    private const float ValueLevelUpIncreaseMultiplier = 1.15f;

    public Image Icon => _icon;
    public int Attack => _attack;
    public int Def => _def;
    public int Health => _health;

    public int Power => Attack + Health;

    public int Level => _level;
    public int Evolution => _evolution;
    public int MaxLevel => _maxLevel;
    public float NextMaxLevelPoitnMultiplier => _nextMaxLevelPointMultiplier;

    public int BonusAttackSkill => (int)(Attack * 0.17f);
    public int Id { get; set; }

    public int LevelPoint => _levelPoint;
    public int MaxLevelPoint => _maxLevelPoint;
    public int AmountIncreaseLevelPoint { get; private set; }

    public virtual Card Card => _card;

    public void Render(Card card)
    {
        _card = card;

        _attack = card.Attack;
        _def = card.Def;
        _health = card.Health;
        _level = card.Level;
        _evolution = 1;
        _maxLevelPoint = card.MaxLevelPoint;

        if (_cardStatsPanel)
        {
            if (Card.Id != 0)
            {
                _cardStatsPanel.gameObject.SetActive(true);
                _cardStatsPanel.Init(Attack.ToString(), Def, Health, _card.SkillIcon);
                _cardName.gameObject.SetActive(true);
                _cardName.text = _card.Name;
                _icon.sprite = card.UIIcon;
            }
            else
            {
                _cardName.gameObject.SetActive(false);
            }
        }

        if (card.Evolution == 1)
            _icon.sprite = _card.ImageFirstEvolution;
        else
            _icon.sprite = _card.ImageSecondeEvolution;
    }

    public void Render(CardCell cardCell)
    {
        _card = cardCell.Card;

        _attack = cardCell.Attack;
        _def = cardCell.Def;
        _health = cardCell.Health;
        _level = cardCell.Level;
        _evolution = cardCell.Evolution;
        _maxLevelPoint = cardCell.MaxLevelPoint;

        if (_cardStatsPanel)
        {
            if (_card.Id != 0)
            {
                _cardStatsPanel.gameObject.SetActive(true);
                _cardStatsPanel.Init(Attack.ToString(), Def, Health, _card.SkillIcon);
                _cardName.gameObject.SetActive(true);
                _cardName.text = _card.Name;
                _icon.sprite = cardCell.Card.UIIcon;
            }
            else
            {
                _cardName.gameObject.SetActive(false);
            }
        }

        if (cardCell.Evolution == 1)
            _icon.sprite = _card.ImageFirstEvolution;
        else
            _icon.sprite = _card.ImageSecondeEvolution;
    }

    public void LevelUp(CardCell[] cardsForEnhance)
    {        
        void LevelUpCardValue()
        {
            _attack = (int)(Attack * ValueLevelUpIncreaseMultiplier);
            _def = (int)(Def * ValueLevelUpIncreaseMultiplier);
            _health = (int)(Health * ValueLevelUpIncreaseMultiplier);
        }

        foreach (var card in cardsForEnhance)
        {
            _levelPoint += card.GetCardDeletePoint();
            AmountIncreaseLevelPoint += card.GetCardDeletePoint();
        }

        while (LevelPoint >= MaxLevelPoint && Level < _maxLevel)
        {
            _levelPoint -= MaxLevelPoint;
            _maxLevelPoint = (int)(MaxLevelPoint * _nextMaxLevelPointMultiplier);
            _level++;
            LevelUpCardValue();
            OnLevelUp?.Invoke();

            Debug.Log("CardCell Current Level Point: " + MaxLevelPoint);
        }

        Render(this);
    }

    public int GetCardDeletePoint()
    {
        float RacialMultiplier(RarityCard race)
        {
            float multiplier = 1;

            for (int i = 1; i < (int)race; i++)
            {
                multiplier += 0.5f;
            }

            return multiplier;
        }

        return (int)(_baseEnhancmentLevelPoint * RacialMultiplier(Card.Rarity) + AmountIncreaseLevelPoint * 0.75f);
    }

    public void Evolve(EvolutionCard firstCard, EvolutionCard secondCard)
    {
        _attack = GetEvolveUpValue(firstCard.CardCell.Attack, secondCard.CardCell.Attack);
        _def = GetEvolveUpValue(firstCard.CardCell.Def, secondCard.CardCell.Def);
        _health = GetEvolveUpValue(firstCard.CardCell.Health, secondCard.CardCell.Health);
        Id = firstCard.CardCell.Id;
        _card = firstCard.CardCell.Card;
        _evolution = 2;
        _level = 1;
        _maxLevelPoint = 1000;
    }

    private int GetEvolveUpValue(int firstValue, int secondValue)
    {
        var average = (firstValue + secondValue) / 2;
        var evolveUpValue = average * ValueIncreaseMultiplier;
        return (int)evolveUpValue;
    }
}
