using EasyUI.PickerWheelUI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteAnimator : MonoBehaviour
{
    [SerializeField] private GameObject[] _cells;

    [SerializeField] private Button _spinButton;
    [SerializeField] private TMP_Text _spinButtonText;

    [SerializeField] private PickerWheel _pickerWheel;

    [SerializeField] private RoulettePage _roulettePage;

    [SerializeField] private Color _invisibleColor, _visibleColor;

    private void OnEnable()
    {
        UnSelectAllCells();
    }

    private void Start()
    {
        _spinButton.onClick.AddListener(() =>
        {
            _spinButton.interactable = false;
            _spinButtonText.color = _invisibleColor;

            _pickerWheel.OnSpinStart(UnSelectAllCells);

            _pickerWheel.OnSpinEnd(wheelPiece =>
            {
                wheelPiece.Select();
                _spinButtonText.color = _visibleColor;
                _spinButton.interactable = true;
                _roulettePage.TakeItem(wheelPiece.Prize);
            });

            _pickerWheel.Spin();
        });
    }

    public void UnSelectAllCells()
    {
        foreach (var cell in _cells)
            cell.SetActive(false);
    }
}
