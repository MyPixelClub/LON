using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardReprisentation : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void Render(Sprite icon)
    {
        _icon.sprite = icon;
    }
}
