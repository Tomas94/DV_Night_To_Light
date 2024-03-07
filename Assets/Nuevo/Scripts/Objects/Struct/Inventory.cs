using System.Collections.Generic;

[System.Serializable]
public struct Inventory
{
    public int healthPill;
    public int staminaPill;
    public int visionPill;
    public int battery;

    public List<NoteEntry> notesList;
}
