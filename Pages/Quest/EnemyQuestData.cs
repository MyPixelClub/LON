using System;
using UnityEngine;

namespace FarmPage.Quest
{
    [Serializable]
    public struct EnemyQuestData
    {
        public EnemyType EnemyType;
        public Sprite View;

        [Range(100, 100000)] 
        public int MaxHealth;

        [Range(100, 100000)] 
        public int Damage;        
    }
}