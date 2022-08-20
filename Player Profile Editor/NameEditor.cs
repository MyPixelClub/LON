using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameEditor : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputerNewName;

    public string GetNewName()
    {
        return _inputerNewName.text;
    }
}
