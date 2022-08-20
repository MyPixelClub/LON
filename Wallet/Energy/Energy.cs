using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private int _currentEnergy;
    [SerializeField] private int _maxEnergy;

    [SerializeField] private EnergyView _energyView;

    [SerializeField] private TMP_Text _timer;

    public int CurrentEnergy => _currentEnergy;
    public int MaxEnergy => _maxEnergy;

    private void OnEnable()
    {
        _energyView.UpdateEnergyValue(this);
    }

    public void DecreaseEnergy(int value)
    {
        if (value > _currentEnergy) throw new ArgumentOutOfRangeException();

        if (value <= _maxEnergy && value > 0)
            _currentEnergy -= value;

        _energyView.UpdateEnergyValue(this);

        StartCoroutine(Timer());
    }

    public void IncreaseEnergy(int value)
    {
        if(value > _maxEnergy || value < 0) throw new ArgumentOutOfRangeException();

        _currentEnergy += value;

        _energyView.UpdateEnergyValue(this);
    }

    private IEnumerator Timer()
    {
        while (_currentEnergy < _maxEnergy)
        {
            TimeSpan leftTime = new(0, 8, 0);

            _timer.text = $"{leftTime.Minutes}:{leftTime.Seconds}";

            while (leftTime != new TimeSpan(0, 0, 0))
            {
                yield return new WaitForSeconds(1f);

                leftTime -= new TimeSpan(0, 0, 1);
                _timer.text = $"{leftTime.Minutes}:{leftTime.Seconds}";

                if (_currentEnergy >= _maxEnergy)
                {
                    _timer.text = "00:00";
                    StopAllCoroutines();
                }
            }

            IncreaseEnergy(1);
        }

        _timer.text = "00:00";
    }
}
