using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Farm
{
    [RequireComponent(typeof(Button), typeof(global::Farm), typeof(PlaceAnimator))]
    public class Place : MonoBehaviour
    {
        [SerializeField] private PlaceData _data;

        [SerializeField] private PlaceInformationWindow _informationWindow;
        [SerializeField] private ListCharacterForSet _listCharacterForSet;

        [SerializeField] private Image _characterImage;

        [SerializeField] private Image _maskImage;
        [SerializeField] private Color _setCharacterColor;
        [SerializeField] private Color _unsetCharacterColor;
        
        
        private bool _isSet;
        private global::Farm _farm;
        private Button _button;

        public PlaceAnimator PlaceAnimator;

        public PlaceData Data => _data;
        public bool IsSet => _isSet;
        public global:: Farm Farm => _farm;

        private void Awake()
        {
            _farm = GetComponent<global::Farm>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => 
            {
                _informationWindow.Render(this);
                _listCharacterForSet.OpenCharacterList(this);
            });
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void UnsetCharacter()
        {             
            _isSet = false;
            _maskImage.color = _unsetCharacterColor;
            _farm.ClaimRewards();
            _informationWindow.Render(this);
        }

        public void SetCharacter(CharacterCell character)
        {
            _maskImage.color = _setCharacterColor;
            _characterImage.sprite = character.CharacterSprite;
            _isSet = true;

            _farm.StartFarm();
            _informationWindow.Render(this);
            _listCharacterForSet.gameObject.SetActive(false);
        }
    }
}
