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
        private EnemyQuestData _enemyQuestData;

        public override int Damage()
        {
            return Random.Range(_enemyQuestData.Damage/2 , _enemyQuestData.Damage);
        }

        public void Init(EnemyQuestData enemyQuestData)
        {
            _enemyQuestData = enemyQuestData;
            _health = _enemyQuestData.MaxHealth;
            _maxHealth = _enemyQuestData.MaxHealth;
            _view.sprite = enemyQuestData.View;
            Init();
        }
    }
}