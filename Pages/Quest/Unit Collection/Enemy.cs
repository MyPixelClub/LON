using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FarmPage.Quest
{
    public enum EnemyType
    {
        Enemy,
        Boss
    }

    public class Enemy : Unit
    {
        [SerializeField] private EnemyFlyAnimation _flyAnimation;
        [SerializeField] private Image _blick;

        [SerializeField] private EnemyPopup[] _variationPopup;

        private EnemyQuestData _enemyQuestData;

        public override int Damage()
        {
            return Random.Range(_enemyQuestData.Damage/2 , _enemyQuestData.Damage);
        }

        public override void TakeDamage(float amountDamage)
        {
            base.TakeDamage(amountDamage);
            StartCoroutine(Blick());
            if(Random.Range(1, 4) == 1)
                _variationPopup[Random.Range(0, _variationPopup.Length)].StartMovement(this);
        }

        public void Init(EnemyQuestData enemyQuestData, HorizontalLayoutGroup horizontalLayoutGroup)
        {
            _enemyQuestData = enemyQuestData;
            _health = _enemyQuestData.MaxHealth;
            _maxHealth = _enemyQuestData.MaxHealth;
            _view.sprite = enemyQuestData.View;
            _flyAnimation.SetStartPosition(horizontalLayoutGroup);
            Init();
        }

        private IEnumerator Blick()
        {
            var color = _blick.color;
            color.a = 1;

            while (color.a != 0)
            {
                color.a -= 0.1f;
                _blick.color = color;
                yield return new WaitForSeconds(0.001f);
            }
        }

        protected override void DecreaseHealth(float amountDamage)
        {
            _health -= amountDamage;
        }
    }
}