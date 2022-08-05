using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameEditor : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputerNewName;
    [SerializeField] private TMP_Text _playerName;

    public void ChangeName()
    {
        _playerName.text = _inputerNewName.text;
    }
}
