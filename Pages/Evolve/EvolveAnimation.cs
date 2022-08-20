using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class EvolveAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _evolvedCard;
    [SerializeField] private Image _evolvedCardImage;

    [SerializeField] private Transform _firstCard, _secondCard;
    [SerializeField] private ParticleSystem _evolvedCardParticle;
    [SerializeField] private float _duration;

    [SerializeField] private Evolution _evolve;

    private Vector2 _firstCardStartPosition, _secondCardStartPosition;

    private void OnEnable()
    {
        _evolve.OnEvolvedCard += MoveCard;
        _firstCardStartPosition = _firstCard.localPosition;
        _secondCardStartPosition = _secondCard.localPosition;
    }

    private void OnDisable()
    {
        _evolve.OnEvolvedCard -= MoveCard;
        _firstCard.localPosition = new(_firstCardStartPosition.x, _firstCardStartPosition.y, 0);
        _secondCard.localPosition = new(_secondCardStartPosition.x, _secondCardStartPosition.y, 0);
        _evolvedCard.SetActive(false);
    }

    private void MoveCard()
    {
        _firstCard.DOMove(_evolvedCard.transform.position, _duration);
        _secondCard.DOMove(_evolvedCard.transform.position, _duration);
        StartCoroutine(DisplayEvolvedCard());
    }

    private IEnumerator DisplayEvolvedCard()
    {
        yield return new WaitForSeconds(_duration);
        _evolvedCard.SetActive(true);
        _evolvedCardImage.sprite = _evolve.EvolvedCardSprite;
        _evolvedCardParticle.Play();
    }
}
