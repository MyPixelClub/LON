using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarsEditor : MonoBehaviour
{
    [SerializeField] private AvatarCell[] _avatars;

    [SerializeField] private Image _playerAvatar;

    public Sprite Avatar { get; private set; }

    private void OnEnable()
    {
        foreach (var avatar in _avatars)
        {
            avatar.GetComponent<Button>().onClick.AddListener(() => SelectAvatar(avatar));
        }
    }

    private void OnDisable()
    {
        foreach (var avatar in _avatars)
        {
            avatar.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    private void SelectAvatar(AvatarCell avatarCell)
    {
        foreach (var avatar in _avatars)
            avatar.DeselectAvatar();

        avatarCell.SelectAvatar();
        Avatar = avatarCell.Avatar;
    }
}
