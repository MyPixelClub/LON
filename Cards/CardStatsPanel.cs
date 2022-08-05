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

        public TMP_Text HealthText => _healthText;

        public int DecreaseHealth(int damage)
        {
            damage -= _defence / 2;
            if (damage < 0) damage = 0;
            _health -= damage;
            if (_health <= 0) _health = 0; ;
            _healthText.text = _health.ToString();
            return _health;
        }

        public void Init(string attack, int defence, int health, Sprite scillIcon)
        {
            _health = health;
            _defence = defence;
            _attackText.text = attack;
            _defenseText.text = defence.ToString();
            _healthText.text = health.ToString();
            _skillImage.sprite = scillIcon;
        }

        private string GetShortNameRarity(string rarity)
        {
            int i = 0;
            char[] chars = new char[rarity.Length];
            foreach (char c in rarity)
                if (char.IsUpper(c))
                    chars[i++] = c;
            return new string(chars, 0, i);
        }
    }
}