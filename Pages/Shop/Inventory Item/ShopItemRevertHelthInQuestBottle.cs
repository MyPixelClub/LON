using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Revert Health In Quest Bottle", menuName = "ScriptableObjects/Shop/Bottle/Revert Health In Quest")]
public class ShopItemRevertHelthInQuestBottle : ShopInventoryItem
{
    public override void UseEffect(Inventory inventory)
    {
        inventory.ReduceAmount(this, 1);
        inventory.PlayerStatisticQuest.ReverHealth(this);
    }
}
