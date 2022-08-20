using Collection;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnValueChanged;

    [SerializeField] private Energy _energy;

    [SerializeField] private Sprite _avatar;

    private List<PlayerBoostingItem> _playerBoostingEffects = new();

    private int _maxExp = 1000, _exp, _level = 1, _health = 1000;
    private string _nickName = "NickName";

    public List<PlayerBoostingItem> PlayerBoostingEffects => _playerBoostingEffects;

    public Sprite Avatar => _avatar;
    public int MaxExp => _maxExp;
    public int EXP => _exp;
    public string NickName => _nickName;
    public int Level => _level;
    public int Health => _health;

    public Energy Energy => _energy;

    private void Start()
    {
        OnValueChanged?.Invoke();
    }

    public void DecreaseEnergy(int energy)
    {
        _energy.DecreaseEnergy(energy);

        OnValueChanged?.Invoke();
    }

    public void IncreaseEXP(int exp)
    {
        if (exp < 0) throw new ArgumentOutOfRangeException();

        _exp = exp;

        while (_exp > _maxExp)
        {
            _exp -= _maxExp;
            _maxExp *= 2;
            _level++;
        }

        OnValueChanged?.Invoke();
    }

    public void ChangeAvarar(Sprite newAvatar)
    {
        _avatar = newAvatar;
        OnValueChanged?.Invoke();
    }

    public void ChangeName(string newName)
    {
        _nickName = newName;
        OnValueChanged?.Invoke();
    }

    public void UseBoostingItem(PlayerBoostingItem boostingItem)
    {
        if (_playerBoostingEffects.Contains(boostingItem)) throw new Exception("This effect alrady exist");

        _playerBoostingEffects.Add(boostingItem);
    }
}
