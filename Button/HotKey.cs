using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image), typeof(Button))]
public class HotKey : MonoBehaviour
{
    private RectTransform _transform;
    private Image _icon;

    private Button _button;
    public Button Button => _button;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _icon = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void Unselect()
    {
        _transform.localScale = new Vector3(1, 1);
        _icon.color = new Color(1, 1, 1, 1);
    }

    public void Select()
    {
        _transform.localScale = new Vector3(1.2f, 1.2f);
        _icon.color = new Color(0.6f, 0.6f, 0.6f, 1);
    }
}
