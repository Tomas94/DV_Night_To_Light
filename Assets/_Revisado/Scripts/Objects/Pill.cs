using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : Pickable_Object
{
    [SerializeField] Consumables _consumable;

    protected override void AddObjectToInventory()
    {
        PillSelector();
        base.AddObjectToInventory();
    }

    void PillSelector()
    {
        switch (_consumable)
        {
            case Consumables.healthPill:
                GameManager.Instance.playerModel._inventory.healthPill++;
                break;
            case Consumables.staminaPill:
                GameManager.Instance.playerModel._inventory.staminaPill++;
                break;
            case Consumables.visionPill:
                GameManager.Instance.playerModel._inventory.visionPill++;
                break;
        }
    }
}