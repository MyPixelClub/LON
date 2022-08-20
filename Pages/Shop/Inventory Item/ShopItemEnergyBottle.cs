using UnityEngine;

[CreateAssetMenu(fileName = "Energy Bottle", menuName = "ScriptableObjects/Shop/Bottle/Energy")]
public class ShopItemEnergyBottle : ShopInventoryItem
{
    [SerializeField] private int _revertEnergyValue;

    public override void UseEffect(Inventory inventory)
    {
        inventory.ReduceAmount(this, 1);
        inventory.Player.Energy.IncreaseEnergy(_revertEnergyValue);
    }
}
