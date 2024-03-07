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
                _player._inventory.healthPill++;
                break;
            case Consumables.staminaPill:
                _player._inventory.staminaPill++;
                break;
            case Consumables.visionPill:
                _player._inventory.visionPill++;
                break;
        }
    }
}