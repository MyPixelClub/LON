using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Card Pack", menuName = "ScriptableObjects/Shop/Card Pack")]
public class ShopItemCardPack : ShopItem, IShopItem
{
    [SerializeField] private Card[] _allStandardCards;
    [SerializeField] private Card[] _allRarityCards;

    [Header("Chance per procent")]
    [SerializeField] private float _dropChance;

    [SerializeField] private string _command;

    public Card[] AllStandardCards => _allStandardCards;
    public Card[] AllRarityCards => _allRarityCards;

    public string ChestCommand => _command;

    public void Buy(IIncreaserWalletValueAndCardsCount increaser)
    {
        Card[] randomCards = GetRandomCards();
        increaser.CardCollection.AddCards(randomCards);
        (increaser as Shop).ConfirmWindow.CardRepresentation(randomCards).Invoke();
    }

    private Card[] GetRandomCards()
    {
        Card[] cards = new Card[Count];

        for (int i = 0; i < Count; i++)
        {
            if (Random.Range(0, Mathf.RoundToInt(1 / (_dropChance / 100))) == 1)
                cards[i] = GetRandomCard(AllRarityCards);
            else
                cards[i] = GetRandomCard(AllStandardCards);
        }

        return cards;
    }

    private Card GetRandomCard(Card[] cards)
    {
        return cards[Random.Range(0, cards.Length)];
    }
}
