using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelection : MonoBehaviour
{
    [SerializeField] private Image _frameImage;
    [SerializeField] private TMP_Text _categoryName;
    [SerializeField] private Color _normalColor, _selectColor;

    public void SelectCategore()
    {
        _categoryName.color = _selectColor;
        _frameImage.gameObject.SetActive(true);
    }

    public void UnselectCategory()
    {
        _categoryName.color = _normalColor;
        _frameImage.gameObject.SetActive(false);
    }
}
