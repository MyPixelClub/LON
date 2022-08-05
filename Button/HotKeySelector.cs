using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotKeySelector : MonoBehaviour
{
    [SerializeField] private List<HotKey> _hotKeys;

    private void Start()
    {
        foreach (var hotKey in _hotKeys)
        {
            hotKey.Button.onClick.RemoveAllListeners();
            hotKey.Button.onClick.AddListener(() => SelectButton(hotKey));
        }
    }

    private void SelectButton(HotKey selectButton)
    {
        foreach (var hotKey in _hotKeys)
            hotKey.Unselect();

        selectButton.Select();
    }
}
