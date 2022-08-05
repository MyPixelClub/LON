using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileEditor : MonoBehaviour
{
    [SerializeField] private Button _avatarEditorButton, _nameEditorButton;
    [SerializeField] private AvatarsEditor _avatarsEditor;
    [SerializeField] private NameEditor _nameEditors;

    [SerializeField] private Button _confirmButton;    

    private void OnEnable()
    {
        SelectAvatarsEditor();

        _avatarEditorButton.onClick.AddListener(() => SelectAvatarsEditor());

        _nameEditorButton.onClick.AddListener(() => SelectNameEditors());
    }

    private void OnDisable()
    {
        _avatarEditorButton.onClick.RemoveAllListeners();
        _nameEditorButton.onClick.RemoveAllListeners();
    }

    private void SelectAvatarsEditor()
    {
        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(_avatarsEditor.ChangeAvatar);
    }

    private void SelectNameEditors()
    {
        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(_nameEditors.ChangeName);
    }
}
