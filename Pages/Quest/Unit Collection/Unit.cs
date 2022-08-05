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

    public bool IsAlive =>
        _health > 0;

    public abstract int Damage();

    public void TakeDamage(int amountDamage)
    {
        if (amountDamage < 0)
            throw new System.AggregateException();

        _health -= amountDamage;

        _healthSliderAnimator.UpdateSlider(_health, MaxHealth, 1, _healthSliderAnimator.Slider.value);

        if (!IsAlive)
            Dead();
    }

    private void Dead()
    {
        _view.color = new Color(0.5f, 0.5f, 0.5f, 1);
        _health = 0;
    }

    protected void Init()
    {
        _view.color = Color.white;
        _healthSlider.value = _health;
        _healthSliderAnimator.UpdateSlider(_health, MaxHealth, 1, _healthSliderAnimator.Slider.value);
    }
}
