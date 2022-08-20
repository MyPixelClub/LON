using DG.Tweening;
using TMPro;
using UnityEngine;
using Collection;
using UnityEngine.UI;

namespace FarmPage.Battle
{
    public class BattleConfirmWindow : MonoBehaviour
    {
        [SerializeField] private BattleController _battle;
        [SerializeField] private GameObject _exeptionBaner;
        [SerializeField] private TMP_Text _exeptionText;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private AttackDeck _attackDeck;

        [SerializeField] private Energy _energy;

        private Vector3 _startPosition;
        private Sequence _sequence;
        private EnemyBattle _enemy;

        private Button _button;
    
        private void Awake()
        {
            _startPosition = transform.localPosition;
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(StartBattle);
            _button.interactable = false;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SelectEnemyCards(EnemyBattle enemy)
        {
            _enemy = enemy;
            _button.interactable = true;
        }

        private void StartBattle()
        {
            if (_energy.CurrentEnergy <= 0)
            {
                _exeptionBaner.SetActive(true);
                _exeptionText.text = "Not enough energy";
                return;
            }

            var isPlayerCardAlive = false;

            foreach (var playerCard in _attackDeck.CardsInDeck)
            {
                isPlayerCardAlive = playerCard.IsSet;
                break;
            }
            
            if (!isPlayerCardAlive)
            {
                _exeptionBaner.SetActive(true);
                _exeptionText.text = "You don't have any heroes in your deck";
                return;
            }

            _battle.gameObject.SetActive(true);
                
            _energy.DecreaseEnergy(5);
            _battle.InitBattle(_enemy);
            _battle.StartFight();
        }

        public void HideSmooth()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.3f))
                .Insert(0, transform.DOLocalMove(_startPosition + new Vector3(0, -120, 0), 0.3f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    
        private void ShowSmooth()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _canvasGroup.alpha = 0;
            transform.localPosition = _startPosition + new Vector3(0, 120, 0);
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.6f))
                .Insert(0, transform.DOLocalMove(_startPosition, 0.5f));
        }
    
        private void OnApplicationQuit() => 
            _sequence?.Kill();
    }
}
