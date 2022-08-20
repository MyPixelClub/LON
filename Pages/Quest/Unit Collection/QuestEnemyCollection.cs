using FarmPage.Quest;
using UnityEngine;
using UnityEngine.UI;

public class QuestEnemyCollection : QuestCollection
{
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;    
    private EnemyQuestData[] _enemyQuestDatas;

    public void Render(EnemyQuestData[] enemyQuestData)
    {
        _enemyQuestDatas = enemyQuestData;
        Render();
    }

    protected override Unit[] GetArrayType()
    {
        _horizontalLayoutGroup.enabled = true;
        return new Enemy[_enemyQuestDatas.Length];
    }

    protected override void InitUnit(Unit unit, int position)
    {
        (unit as Enemy).Init( _enemyQuestDatas[position], _horizontalLayoutGroup);
    }
}
