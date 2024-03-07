using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisibility : LightInteractable
{
    public List<GameObject> objects;
    bool _activated = false;

    public override void ActivateInteraction()
    {
        if(_activated) return;
        _activated = true;
        foreach (var obj in objects)
        {
            obj.SetActive(obj.activeSelf ? false : true);
            Debug.Log("el Objeto " + obj.name + " esta " + obj.activeSelf);
        }
    }
}
