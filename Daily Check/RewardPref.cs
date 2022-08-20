using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPref : MonoBehaviour
{
    [SerializeField] private Image _backGround, _currentStreakFrame;
    [SerializeField] private Sprite _normalSprite, _takenSprite;

    [SerializeField] private TMP_Text _dayText, _rewardValue;

    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Sprite _rewardGold, _rewardCristal;

    public void SetRewardData(int day, int currentStreak, Prize reward, bool canClaimRewards)
    {
        _currentStreakFrame.gameObject.SetActive(false);

        _dayText.text = $"Day {day + 1}";

        _rewardIcon.sprite = reward.UIIcon;
        _rewardValue.text = reward.AmountPrize.ToString();

        _backGround.sprite = day < currentStreak ? _takenSprite : _normalSprite;

        if (canClaimRewards && day == currentStreak)
            _currentStreakFrame.gameObject.SetActive(true);
    }
}
