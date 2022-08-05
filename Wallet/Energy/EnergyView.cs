using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentEnergyText, _maxEnergyText;

    public void UpdateEnergyValue(Energy energy)
    {
        _currentEnergyText.text = energy.CurrentEnergy.ToString();
        _maxEnergyText.text = energy.MaxEnergy.ToString();
    }
}
