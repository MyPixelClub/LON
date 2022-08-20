using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    public Sprite UIIcon { get; }

    void UseEffect(Inventory inventory);
}
