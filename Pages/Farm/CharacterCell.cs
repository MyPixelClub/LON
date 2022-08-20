using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterCell : MonoBehaviour
{
    public event UnityAction<CharacterCell> OnSelected;

    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    [SerializeField] private Color _selectColor;

    public Sprite CharacterSprite => _image.sprite;

    private void OnEnable()
    {
        _button.onClick.AddListener(() => OnSelected?.Invoke(this));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Render(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void Select()
    {
        _image.color = _selectColor;
    }

    public void UnSelect()
    {
        _image.color = new Color(1,1,1,1);
    }
}
