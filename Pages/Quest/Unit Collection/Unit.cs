using DG.Tweening;
using FarmPage.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected Image _view;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private SliderAnimator _healthSliderAnimator;    

    protected float _health;
    protected float _maxHealth;
    private Vector3 _localPosition;
    private Vector3 _scale;

    public float MaxHealth => _maxHealth;
    public float Health => _health;

    public bool IsAlive => _health > 0;
    public bool IsShake { get; private set; }

    public abstract int Damage();

    public virtual void TakeDamage(float amountDamage)
    {
        if (amountDamage < 0)
            throw new System.AggregateException();

        DecreaseHealth(amountDamage);        

        _healthSliderAnimator.UpdateSlider(_health, MaxHealth, 1, _healthSliderAnimator.Slider.value);

        StartCoroutine(Shake());

        if (!IsAlive)
            Dead();
    }

    private void Dead()
    {
        _view.color = new Color(0.5f, 0.5f, 0.5f, 1);
        _health = 0;
    }

    private IEnumerator Shake()
    {
        var startLocalPosition = transform.localPosition;
        IsShake = true;

        for (int i = 0; i < 10; i++)
        {
            var multiplier = 1 - (i / 9);

            transform.DOLocalMove(transform.localPosition.RandomVector2(10 * multiplier), 0.005f);
            yield return new WaitForSeconds(0.005f);
            transform.DOLocalMove(startLocalPosition, 0.005f);
            yield return new WaitForSeconds(0.005f);
        }

        IsShake = false;
    }

    public IEnumerator Selected()
    {
        var sequence = DOTween.Sequence();
        IsShake = true;
        _localPosition = transform.localPosition;
        _scale = transform.localScale;

        sequence
            //.Insert(0, _view.DOColor(new Color(1, 1, 1, 0.5f), 0.5f))
            .Insert(0, transform.DOLocalMove(_localPosition + new Vector3(0, 200, 0), 0.4f))
            .Insert(0, transform.DOScale(_scale * 1.4f, 0.4f));
        //.Insert(0.5f, _view.DOColor(Color.clear, 0.5f));

        yield return new WaitForSeconds(1f);

        Unselected();
    }

    private void Unselected()
    {
        var sequence = DOTween.Sequence();

        sequence
            .Insert(0, transform.DOLocalMove(_localPosition, 0.4f))
            .Insert(0, transform.DOScale(_scale, 0.4f));

        IsShake = false;
    }

    protected void Init()
    {
        _view.color = Color.white;
        _healthSlider.value = _health;
        _healthSliderAnimator.UpdateSlider(_health, MaxHealth, 1, _healthSliderAnimator.Slider.value);
    }

    protected abstract void DecreaseHealth(float amountDamage);
}
