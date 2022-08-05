using Collection;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnValueChanged;

    [SerializeField] private Energy _energy;

    [SerializeField] private Sprite _avatar;

    private int _maxExp = 100, _exp, _level = 1, _health = 100;
    private string _nickName = "NickName";

    public Sprite Avatar => _avatar;
    public int MaxExp => _maxExp;
    public int EXP => _exp;
    public string NickName => _nickName;
    public int Level => _level;
    public int Health => _health;

    public Energy Energy => _energy;
    
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
}
