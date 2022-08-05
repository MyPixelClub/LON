using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AvatarCell : MonoBehaviour
{
    [SerializeField] private Image _frame, _avatar;
    [SerializeField] private Color _normalColor, _selectColor;

    public Sprite Avatar { get; private set; }

    private void OnDisable()
    {
        DeselectAvatar();
    }

    public void SelectAvatar()
    {
        _frame.color = _selectColor;
        Avatar = _avatar.sprite;
    }

    public void DeselectAvatar()
    {
        _frame.color = _normalColor;
        Avatar = null;
    }
}
