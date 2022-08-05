using FarmPage.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestEnemyCollection : QuestCollection
{
    private EnemyQuestData[] _enemyQuestDatas;

    public void Render(EnemyQuestData[] enemyQuestData)
    {
        _enemyQuestDatas = enemyQuestData;
        Render();
    }

    protected override Unit[] GetArrayType()
    {
        return new Enemy[_enemyQuestDatas.Length];
    }

    protected override void InitUnit(Unit unit, int position)
    {
       (unit as Enemy).Init( _enemyQuestDatas[position]);
    }
}
