using System;
using System.Collections;
using System.Collections.Generic;
using FarmPage;
using FarmPage.Farm;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlaceInformationWindow : MonoBehaviour
{
    [SerializeField] private Image _locationImage;
    [SerializeField] private TMP_Text _locationDiscription;

    [SerializeField] private TMP_Text _statusText;

    [SerializeField] private Button _setOrUnsetCharacterButton, _closeButton;
    [SerializeField] private TMP_Text _setOrUnsetCharacterButtonText;

    [SerializeField] private ListCharacterForSet _characterList;    

    [SerializeField] private PrizeWindow _prizeWindow;
    [SerializeField] private PlaceAnimator[] _placeAnimators;

    [SerializeField] private PosiblePrizesWindow _posiblePrizesWindow;
    [SerializeField] private CooldownSelector _cooldownSelector;  
    
    private Farm _farm;

    private Action _setCharacterInPlace;
    public Action SetCharacterInPlace
    {
        get
        {
            return _setCharacterInPlace;
        }
        set
        {
            _setCharacterInPlace = value;
            RenderButton(_farm.Place);
        }
    }

    private void OnEnable()
    {
        _cooldownSelector.OnCooldownChanged += SetCooldownMultiplyer;
    }

    private void OnDisable()
    {
        _cooldownSelector.OnCooldownChanged -= SetCooldownMultiplyer;
        _farm.OnFarmFinished -= Render;
        SetCharacterInPlace = null;
    }

    public void Render(Place place)
    {
        gameObject.SetActive(true);

        foreach (var placeAnimator in _placeAnimators)
        {
            placeAnimator.Unpressed();
            placeAnimator.UnSelected();
        }            

        if (_farm != null)
        {
            _farm.OnTimerChanged -= RenderStatusText;
            _farm.OnFarmFinished -= Render;
        }

        _farm = place.Farm;
        place.PlaceAnimator.Pressed();

        _farm.OnTimerChanged += RenderStatusText;
        _farm.OnFarmFinished += Render;

        _posiblePrizesWindow.RenderPrize(place);
        RenderLocation(place);
        RenderButton(place);
        RenderStatusText();        
    }    

    private void RenderLocation(Place place)
    {
        _locationImage.sprite = place.Data.LocationImage;
        _locationDiscription.text = place.Data.Discription;
    }

    private void RenderButton(Place place)
    {
        _setOrUnsetCharacterButton.onClick.RemoveAllListeners();
        _cooldownSelector.gameObject.SetActive(false);

        if (_farm.CanClaimRewared)
        {
            _setOrUnsetCharacterButton.gameObject.SetActive(true);
            _setOrUnsetCharacterButton.onClick.AddListener(() =>
            {
                place.UnsetCharacter();
                gameObject.SetActive(false);                
                _prizeWindow.Render(_posiblePrizesWindow.CurrentRandomPrizes);
            });
            _setOrUnsetCharacterButtonText.text = "Claim";
            return;
        }

        if (place.IsSet == false)
        {
            if (SetCharacterInPlace != null)
            {
                _setOrUnsetCharacterButton.gameObject.SetActive(true);
                _setOrUnsetCharacterButtonText.text = "Start";
                _setOrUnsetCharacterButton.onClick.AddListener(() =>
                {
                    _farm.SetCooldown(_cooldownSelector.Cooldown);
                    SetCharacterInPlace.Invoke();
                    _setOrUnsetCharacterButton.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                });
                _cooldownSelector.gameObject.SetActive(true);
            }
            else
            {
                _setOrUnsetCharacterButton.gameObject.SetActive(false);
            }
        }       

    }   

    private void RenderStatusText()
    {
        if (_farm.Place.IsSet)
            _statusText.text = _farm.Status;
        else
            _statusText.text = "";
    }

    private void SetCooldownMultiplyer()
    {
        _posiblePrizesWindow.RenderPrize(_farm.Place);
    }
}
