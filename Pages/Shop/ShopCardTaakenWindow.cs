using System.Collections.Generic;
using UnityEngine;

public class ShopCardTaakenWindow : MonoBehaviour
{
    [SerializeField] private ShopCardReprisentation _card;
    [SerializeField] private Transform _container;
    [SerializeField] private ParticleSystem _stars;

    public void Render(List<Card> cards)
    {
        gameObject.SetActive(true);
        _stars.Play();

        foreach (Transform item in _container)
            Destroy(item.gameObject);

        foreach (var card in cards)
            Instantiate(_card, _container).Render(card.UIIcon);
    }
}
