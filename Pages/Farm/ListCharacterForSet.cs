using System.Collections.Generic;
using UnityEngine;
using System;
using FarmPage.Farm;

public class ListCharacterForSet : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private CharacterCell _characterCellTamplate;
    [SerializeField] private List<Sprite> _nftSprites;

    [SerializeField] private PlaceInformationWindow _informationWindow;

    private List<CharacterCell> _characterCells = new();

    private Action<CharacterCell> _setCharacter;
    private CharacterCell _selectionCharacter;
    
    private void OnDisable()
    {
        foreach (var cell in _characterCells.ToArray())
        {
            cell.OnSelected -= SelectCharacter;
            Destroy(cell.gameObject);
        }

        _characterCells.Clear();
    }

    public void OpenCharacterList(Place place)
    {
        if (place.IsSet) return;

        gameObject.SetActive(true);

        Render(_nftSprites);

        _setCharacter = place.SetCharacter;
    }

    private void SelectCharacter(CharacterCell character)
    {
        foreach (var cell in _characterCells)
            cell.UnSelect();

        character.Select();

        _selectionCharacter = character;
        _informationWindow.SetCharacterInPlace = SetCharacter;
    }

    private void SetCharacter()
    {
        _setCharacter.Invoke(_selectionCharacter);
        gameObject.SetActive(false);
    }

    private void Render(List<Sprite> spriteForRender)
    {
        foreach (var sprite in spriteForRender)
        {
            var cell = Instantiate(_characterCellTamplate, _container);
            cell.Render(sprite);
            _characterCells.Add(cell);
            cell.OnSelected += SelectCharacter;
        }
    }
}
