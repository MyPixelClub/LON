using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticRank : PlayerStatistic
{
    protected override void UpdateDisplay()
    {
        _avatar.sprite = _player.Avatar;
        _nickName.text = _player.NickName;
    }
}
