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

    protected int _health;
    protected int _maxHealth;

    public float MaxHealth => _maxHealth;
    public int Health => _health;

    public bool IsAlive => _health > 0;
    public bool IsShake { get; private set; }
    public abstract int Damage();

    public virtual void TakeDamage(int amountDamage)
    {
        if (amountDamage < 0)
            throw new System.AggregateException();

        _health -= amountDamage;

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

    protected void Init()
    {
        _view.color = Color.white;
        _healthSlider.value = _health;
        _healthSliderAnimator.UpdateSlider(_health, MaxHealth, 1, _healthSliderAnimator.Slider.value);
    }
}
