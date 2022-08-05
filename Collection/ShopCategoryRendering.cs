using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using FarmPage.Shop;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CategorySelection))]
public class ShopCategoryRendering : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private ShopItemCell _shopItemCellTemplate;
    [SerializeField] private Transform _container;
    
    [SerializeField] private ConfirmWindow _confirmWindow;

    [SerializeField] private CategorySelection _categorySelection;

    public CategorySelection CategorySelection => _categorySelection;
    
    private void OnEnable()
    {
        _confirmWindow.gameObject.SetActive(false);
    }

    public void SelectCategore()
    {
        _categorySelection.SelectCategore();
        Render();
    }

    private void Render()
    {
        foreach (Transform childs in _container)
            Destroy(childs.gameObject);


        _shopItems.ForEach(item =>
        {
            var cell = Instantiate(_shopItemCellTemplate, _container);
            cell.Init(_confirmWindow);
            cell.Render(item as IShopItem);
        });

        _confirmWindow.gameObject.SetActive(false);
    }
}
