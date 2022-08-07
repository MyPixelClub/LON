using UnityEngine;
using UnityEngine.UI;

public interface ICard
{  
    public Card Card { get; }
    public Sprite UIIcon { get; }
    public Sprite Frame { get; }

    public int Attack { get; }
    public int Def { get; }
    public int Health { get; }
    public int Level { get; }
    public int Evolution { get; }
    public int LevelPoint { get; }
    public int MaxLevelPoint { get; }

    public int BonusAttackSkill { get; }
    public int Id { get; set; }
}
