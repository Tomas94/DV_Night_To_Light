using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Inventory _savedInventory = new Inventory();
    [SerializeField] Transform _spawnPoint;

    void SaveStatus(PlayerLogic player)
    {
        _savedInventory.battery = player._inventory.battery;
        _savedInventory.healthPill = player._inventory.healthPill;
        _savedInventory.visionPill = player._inventory.visionPill;
        _savedInventory.staminaPill = player._inventory.staminaPill;
        _savedInventory.notesList = player._inventory.notesList;
        GameManager.Instance.Checkpoint = this;
    }

    public void LoadStatus(PlayerLogic player)
    {
        player._inventory.battery = _savedInventory.battery;
        player._inventory.healthPill = _savedInventory.healthPill;
        player._inventory.visionPill = _savedInventory.visionPill;
        player._inventory.staminaPill = _savedInventory.staminaPill;
        player._inventory.notesList = _savedInventory.notesList;
        player.transform.position = _spawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerLogic>(out PlayerLogic player)) SaveStatus(player);
    }
}