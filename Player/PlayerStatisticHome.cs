using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatisticHome : PlayerStatistic
{
    [SerializeField] private SliderAnimator _expSlider;
    [SerializeField] private TMP_Text _expText;
    protected override void UpdateDisplay()
    {
        base.UpdateDisplay();
        
        _expSlider.UpdateSlider(_player.EXP, _player.MaxExp);
        _expText.text = $"{_player.EXP}/{_player.MaxExp}";
    }
}
