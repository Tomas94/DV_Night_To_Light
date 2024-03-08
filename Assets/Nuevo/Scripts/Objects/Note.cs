using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Note : Pickable_Object
{
    public NoteEntry entry = new NoteEntry();
    [SerializeField] Notas_UI _textWindow;
    bool _isReading = false;

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(SetToManager), 0.8f);
    }

    protected override void AddObjectToInventory()
    {
        if (_isReading) return;
        ReadNote();
        if (_player._inventory.notesList.Contains(entry)) return;
        _player._inventory.notesList.Add(entry);

    }

    void ReadNote()
    {
        _textWindow._Background.gameObject.SetActive(true);
        _textWindow._title.text = entry.title;
        _textWindow._description.text = entry.description;
        StartCoroutine(nameof(ReadingNote));
    }


    IEnumerator ReadingNote()
    {
        Time.timeScale = 0;
        _isReading = true;
        while (_isReading)
        {
            if (Input.GetMouseButtonDown(0)) _isReading = false;
            yield return null;
        }
        _textWindow._Background.gameObject.SetActive(false);
        Time.timeScale = 1;
    }


    void SetToManager()
    {
        _textWindow = GameManager.Instance.NoteBox;
    }
}
