using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour , Pickable
{
    [SerializeField] PillVariants _pillType;

    public void Pickup()
    {
        //Codigo para agregar uno al inventario.
        //Necesito Player y UI
        Debug.Log("Pickeado un " + this.name);
    }
}

enum PillVariants
{
    health,
    stamina,
    vision
}