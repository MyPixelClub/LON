using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tabs;
    [SerializeField] private List<CategorySelection> _categoryButton;

    public void SelectCategory(CategorySelection selectCategoryButton)
    {
        foreach (var categoryButton in _categoryButton)
        {
            categoryButton.UnselectCategory();
        }

        selectCategoryButton.SelectCategore();
    }

    public void SelectTab(GameObject category)
    {
        foreach (var tab in _tabs)
            tab.SetActive(false);

        category.SetActive(true);
    }
}