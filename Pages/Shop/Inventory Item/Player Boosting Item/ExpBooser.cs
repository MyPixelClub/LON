using UnityEngine;

[CreateAssetMenu(fileName = "Exp Booster", menuName = "ScriptableObjects/Shop/Player Boosting Effects/Exp Boosting")]
public class ExpBooser : PlayerBoostingItem
{
    public override void UseEffect(Inventory inventory)
    {
        if (inventory.Player.PlayerBoostingEffects.Contains(this) == false)
        {
            inventory.Player.UseBoostingItem(this);
            inventory.ReduceAmount(this, 1);
        }
    }
}
