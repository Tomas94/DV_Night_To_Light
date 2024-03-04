using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Pickable_Object
{
    protected override void AddObjectToInventory()
    {
        GameManager.Instance.playerModel._inventory.battery++;
        base.AddObjectToInventory();
    }
}
