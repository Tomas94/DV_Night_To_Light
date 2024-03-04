using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Note : Pickable_Object
{
    public NoteEntry entry = new NoteEntry();

    protected override void AddObjectToInventory()
    {
        List<NoteEntry> noteList = GameManager.Instance.playerModel._inventory.notesList;
        if (noteList.Contains(entry)) return;

        noteList.Add(entry);
        Debug.Log("Titulo: " + noteList.Last().title + ". Texto: " + noteList.Last().description);
        base.AddObjectToInventory();
    }

}
