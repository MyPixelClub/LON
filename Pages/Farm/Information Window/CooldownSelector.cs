using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CooldownSelector : MonoBehaviour
{
    public event UnityAction OnCooldownChanged;

    [SerializeField] private PlaceInformationWindow _informationWindow;
    [SerializeField] private ListCharacterForSet _listCharacterForSet;

    [SerializeField] private Button[]  _cooldownButtons;

    private Button _currentCooldownButton;

    public int PrizeMultiplyer { get; private set; }
    public float Cooldown { get; private set; }

    private void Start()
    {
        PrizeMultiplyer = 1;
    }

    private void OnEnable()
    {
        foreach (var cooldown in _cooldownButtons)
            cooldown.onClick.AddListener(() => _currentCooldownButton = cooldown);

        if(_currentCooldownButton != null)
            _currentCooldownButton.Select();
    }

    private void OnDisable()
    {
        foreach (var button in _cooldownButtons)
            button.onClick.RemoveAllListeners();
    }

    public void SetCooldown(float valuePerMinutes)
    {
        PrizeMultiplyer = (int)(valuePerMinutes / 5f);
        Cooldown = valuePerMinutes;
        OnCooldownChanged?.Invoke();
    }
}
