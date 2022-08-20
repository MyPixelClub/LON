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

        [SerializeField] private EnemyPopup[] _variationPopup;

        [SerializeField] private Image _blickImage;

        private EnemyQuestData _enemyQuestData;

        public override int Damage()
        {
            return Random.Range(_enemyQuestData.Damage/2 , _enemyQuestData.Damage);
        }

        public override void TakeDamage(float amountDamage)
        {
            base.TakeDamage(amountDamage);            
            if(Random.Range(1, 4) == 1)
                _variationPopup[Random.Range(0, _variationPopup.Length)].StartMovement(this);
        }

        public void Init(EnemyQuestData enemyQuestData, HorizontalLayoutGroup horizontalLayoutGroup)
        {
            _blick = _blickImage;
            _enemyQuestData = enemyQuestData;
            _health = _enemyQuestData.MaxHealth;
            _maxHealth = _enemyQuestData.MaxHealth;
            _view.sprite = enemyQuestData.View;
            _flyAnimation.SetStartPosition(horizontalLayoutGroup);
            Init();
        }

        

        protected override void DecreaseHealth(float amountDamage)
        {
            _health -= amountDamage;
        }
    }
}