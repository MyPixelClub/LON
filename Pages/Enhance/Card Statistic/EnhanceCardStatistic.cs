using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Enhance.Card_Statistic
{
    public abstract class EnhanceCardStatistic : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _atk, _def, _health, _level;

        protected virtual void OnDisable()
        {
            _atk.text = "";
            _def.text = "";
            _health.text = "";
            _level.text = "";
        }

        public void Render(CardCell cardForDelete)
        {
            _icon.sprite = cardForDelete.UIIcon;
            _atk.text = "ATK: " + cardForDelete.Attack;
            _def.text = "DEF: " + cardForDelete.Def;
            _health.text = "HP: " + cardForDelete.Health;
            _level.text = cardForDelete.Level.ToString();
        }
    }
}
