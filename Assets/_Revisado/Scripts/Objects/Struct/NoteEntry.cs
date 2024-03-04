using UnityEngine;

[System.Serializable]
public struct NoteEntry
{
    [TextArea(2,3)]
    public string title;
    
    [TextArea(5, 15)]
    public string description;
}
