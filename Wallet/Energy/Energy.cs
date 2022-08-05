using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private int _currentEnergy;
    [SerializeField] private int _maxEnergy;

    [SerializeField] private EnergyView _energyView;

    public int CurrentEnergy => _currentEnergy;
    public int MaxEnergy => _maxEnergy;

    private void OnEnable()
    {
        _energyView.UpdateEnergyValue(this);
    }

    public void DecreaseEnergy(int value)
    {
        if (value <= _maxEnergy && value > 0)
            _currentEnergy -= value;

        _energyView.UpdateEnergyValue(this);
    }
}
