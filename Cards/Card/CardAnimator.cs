using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace Cards.Card
{
    public class CardAnimator : MonoBehaviour
    {
        private static readonly int _smoke = Animator.StringToHash("Smoke");

        [SerializeField] 
        private Image _image;

        [SerializeField] private Image _deadImage;

        [SerializeField] 
        private Sprite _sideBackSprite;

        [SerializeField] 
        private Image _lightImage;

        [SerializeField] 
        private Image _hitImage;

        [SerializeField] 
        private Image _stateImage;

        [SerializeField] 
        private Image _shadow;

        [SerializeField] 
        private Image _selectImage;
        
        [SerializeField] private Image _magicCircleImage;
        [SerializeField] private ParticleSystem _fallAnimation;

        [SerializeField]
        private ParticleSystem _flyEffect;
        
        [SerializeField]
        private Animator _smokeEffect;

        [SerializeField]
        private Transform _effectContainer;

        [SerializeField] 
        private TextMeshProUGUI[] _damageTexts;

        [SerializeField] 
        private Sprite[] _frames;

        [SerializeField] 
        private Animator _animator;

        [SerializeField] private CardStatsPanel _cardStatsPanel;
        [SerializeField] private GameObject _frame;

        [SerializeField] private Color _numberNormalColor;

        private Vector3 _scale;
        private Vector3 _localPosition;

        public int HealthLeft { get; private set; }
        public global::Card Card { get; private set; }

        private void Start()
        {
            _shadow.gameObject.SetActive(false);
            transform.localPosition = transform.localPosition.ToY(100);
            Hide();
        }

        private void OnEnable()
        {
            _animator.enabled = true;
            HealthLeft = _cardStatsPanel.Health;
            _deadImage.DOColor(new Color(0,0,0,0), 0);
        }

        private void OnDisable()
        {
            _animator.enabled = false;
        }

        public void Init(global::Card card)
        {
            Card = card;

            _image.sprite = card.UIIcon;
            _shadow.sprite = _image.sprite;
            _cardStatsPanel.Init(card.Attack.ToString(), card.Def, card.Health, card.SkillIcon);
            _frame.gameObject.SetActive(false);
            _cardStatsPanel.gameObject.SetActive(false);
        }

        public IEnumerator StartIntro(Sequence sequence, float y)
        {
            _magicCircleImage.gameObject.SetActive(true);

            gameObject.SetActive(true);
            _image.gameObject.SetActive(false);
            _shadow.sprite = _image.sprite;
            _image.color = Color.clear;

            _animator.SetTrigger("Intro");
            yield return new WaitForSeconds(2f);

            _magicCircleImage.gameObject.SetActive(false);

            _fallAnimation.Play();

            yield return new WaitForSeconds(0.5f);

            _frame.gameObject.SetActive(true);
            _cardStatsPanel.gameObject.SetActive(true);
            _image.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);
            _smokeEffect.GetComponent<Image>().enabled = false;
            
            _localPosition = transform.localPosition;
            _scale = transform.localScale;
        }

        public IEnumerator Hit(ParticleSystem attackEffect, int attack)
        {
            var effect = Instantiate(attackEffect, _effectContainer);
            effect.Play();
            yield return Shake();

            yield return new WaitForSeconds(0.3f);
    
            var damageText = _damageTexts[0];
            _cardStatsPanel.DecreaseHealth(attack);
            HealthLeft = _cardStatsPanel.Health;
            var finelDamage = _cardStatsPanel.DamageAfterRessist;

            if (finelDamage <= 0)
                damageText.DOColor(Color.gray, 0.3f);
            else
                damageText.DOColor(new Color(1, 0, 0, 1), 0.3f);

            damageText.text = '-' + finelDamage.ToString();
            _cardStatsPanel.HealthText.color = Color.red;
            
            yield return new WaitForSeconds(0.6f);
            damageText.DOColor(new Color(1, 0, 0, 0), 0.3f);
            yield return new WaitForSeconds(0.3f);

            Destroy(effect);

            _cardStatsPanel.HealthText.color = _numberNormalColor;

            if (HealthLeft == 0)
                _deadImage.DOColor(new Color(0,0,0, 0.5f), 1);
        }

        private IEnumerator Shake()
        {
            var startLocalPosition = transform.localPosition;
            
            for (int i = 0; i < 10; i++)
            {
                var multiplier = 1 - (i / 9);

                transform.DOLocalMove(transform.localPosition.RandomVector2(10 * multiplier), 0.005f);
                yield return new WaitForSeconds(0.005f);
                transform.DOLocalMove(startLocalPosition, 0.005f);
                yield return new WaitForSeconds(0.005f);
            }
        }

        public void Selected()
        {
            var sequence = DOTween.Sequence();

            sequence
                .Insert(0, _selectImage.DOColor(new Color(1, 1, 1, 0.5f), 0.5f))
                .Insert(0, transform.DOLocalMove(_localPosition + new Vector3(0, 50, 0), 0.2f))
                .Insert(0, transform.DOScale(_scale * 1.2f, 0.2f))
                .Insert(0.5f, _selectImage.DOColor(Color.clear, 0.5f));
        }

        public void Unselected()
        {
            var sequence = DOTween.Sequence();

            sequence
                .Insert(0, transform.DOLocalMove(_localPosition, 0.2f))
                .Insert(0, transform.DOScale(_scale, 0.2f));
        }

        public void Hide()
        {
            _image.color = Color.clear;
            _magicCircleImage.color = Color.white;
        }
    }
}