public class Battery : Pickable_Object
{
    protected override void AddObjectToInventory()
    {
        _player._inventory.battery++;
        base.AddObjectToInventory();
    }
}
