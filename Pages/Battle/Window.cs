using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Battle
{
    public class Window : MonoBehaviour
    {
        [SerializeField] 
        private CanvasGroup _canvasGroup;

        [SerializeField] private Image _playerAvatarInRank, _playerAvatarInWindow, _enemyAvatarInWindow;

        [SerializeField] private TMP_Text _nameInRank, _nameInWindow, _enemyName;
        
        private Vector3 _startPosition;
        private Sequence _sequence;
        
        public void HideSmooth()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 0.3f))
                .Insert(0, transform.DOLocalMove(_startPosition + new Vector3(0, -120, 0), 0.3f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    
        public void ShowSmooth(EnemyBattle enemy)
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
        
            _canvasGroup.alpha = 0;
            transform.localPosition = _startPosition + new Vector3(0, 120, 0);
            _sequence
                .Insert(0, DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.6f))
                .Insert(0, transform.DOLocalMove(_startPosition, 0.5f));

            Render(enemy);
        }

        private void Render(EnemyBattle enemy)
        {
            _playerAvatarInWindow.sprite = _playerAvatarInRank.sprite;
            _nameInWindow.text = _nameInRank.text;

            _enemyAvatarInWindow.sprite = enemy.Avatar;
            _enemyName.text = enemy.Name;
        }
    }
}