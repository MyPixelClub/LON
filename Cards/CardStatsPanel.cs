using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardStatsPanel : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _attackText;

        [SerializeField] 
        private TextMeshProUGUI _defenseText;

        [SerializeField] 
        private TextMeshProUGUI _healthText;

        [SerializeField] private Image _skillImage;

        private int _health;
        private int _defence;

        public int Defence => _defence;
        public int Health => _health;
        public int DamageAfterRessist { get; private set; }
        public TMP_Text HealthText => _healthText;

        public void Init(string attack, int defence, int health, Sprite scillIcon)
        {
            _health = health;
            _defence = defence;
            _attackText.text = attack;
            _defenseText.text = defence.ToString();
            _healthText.text = health.ToString();
            _skillImage.sprite = scillIcon;
        }

        public void DecreaseHealth(int damage)
        {          
            _health -= GetDamageValueAfterResist(damage);

            if (_health <= 0) _health = 0; ;
            _healthText.text = _health.ToString();
        }

        private int GetDamageValueAfterResist(float amountDamage)
        {
            amountDamage -= Random.Range(_defence / 2, _defence);

            if (amountDamage < 0) amountDamage = 0;

            DamageAfterRessist = (int)amountDamage;

            return DamageAfterRessist;
        }
    }
}