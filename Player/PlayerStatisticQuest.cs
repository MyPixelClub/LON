using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticQuest : PlayerStatistic
{
    [SerializeField] private SliderAnimator _healthSliderAnimator;
    [SerializeField] private SliderAnimator _expSlider;

    private int _health;
    private int _maxHealth;

    protected override void OnEnable()
    {
        base.OnEnable();
        _health = _player.Health;
        _maxHealth = _player.Health;
    }

    protected override void UpdateDisplay()
    {
        base.UpdateDisplay();

        _expSlider.UpdateSlider(_player.EXP, _player.MaxExp);
                
    }

    private void TakeDamage(int amountDamage)
    {
        if (amountDamage < 0) throw new System.ArgumentOutOfRangeException();

        _health -= amountDamage;

        _healthSliderAnimator.UpdateSlider(_health, _maxHealth, 1, _healthSliderAnimator.Slider.value);
    }
}
