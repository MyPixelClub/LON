using System.Collections;
using System.Globalization;
using Collection;
using Data;
using DG.Tweening;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public abstract class PlayerStatistic : MonoBehaviour
{
    [SerializeField] protected TMP_Text _nickName, _levelText;
    [SerializeField] protected Image _avatar;
    

    [SerializeField] protected Player _player;

    protected virtual void OnEnable()
    {
        UpdateDisplay();
        _player.OnValueChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        _player.OnValueChanged -= UpdateDisplay;
    }

    protected virtual void UpdateDisplay()
    {
        _avatar.sprite = _player.Avatar;
        _nickName.text = _player.NickName;
        _levelText.text = _player.Level.ToString();        
    }
}
