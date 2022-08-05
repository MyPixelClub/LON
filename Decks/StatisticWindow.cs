using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Collection;
using FarmPage.Enhance;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _atk, _def, _name, _health;
    [SerializeField] private AttackDeck _attackDeck;
    [SerializeField] private DefenceDeck _defDeck;
    [SerializeField] private Button _equipButton;
    [SerializeField] private TMP_Text _equipButtonText;
    [SerializeField] private Image _equipButtonImage;
    [SerializeField] private Sprite _equipButtonSprite;
    [SerializeField] private Sprite _disarmButtonSprite;

    public void Render(CardCellInDeck card)
    {
        _equipButton.gameObject.SetActive(true);
        _equipButton.onClick.RemoveAllListeners();
        Render((CardCell)card);
        _equipButtonImage.sprite = _disarmButtonSprite;
        _equipButtonText.text = "disarm";
        _equipButton.onClick.AddListener(() =>
        {
            card.ResetFromDeck.Invoke();
            gameObject.SetActive(false);
        });
    }

    public void Render(CardCollectionCell card)
    {
        _equipButton.gameObject.SetActive(true);
        _equipButton.onClick.RemoveAllListeners();
        Render((CardCell)card);
        _equipButtonImage.sprite = _equipButtonSprite;
        _equipButtonText.text = "equip";
        _equipButton.onClick.AddListener(() =>
        {
            if (_attackDeck.gameObject.activeInHierarchy)
                _attackDeck.SetCardInDeck(card);
            else
                _defDeck.SetCardInDeck(card);
            gameObject.SetActive(false);
        });
    }

    public void Render(EnchanceCardCell enchanceCardCell)
    {
        Render((CardCell)enchanceCardCell);
        _equipButton.gameObject.SetActive(false);
    }

    private void Render(CardCell cardCell)
    {
        gameObject.SetActive(true);

        _icon.sprite = cardCell.Icon.sprite;

        _atk.text =  cardCell.Attack.ToString();
        _def.text =  cardCell.Def.ToString();
        _health.text = cardCell.Health.ToString();
        _name.text = cardCell.Card.Name;
    }
}
