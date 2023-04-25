using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]PlayerInventory inventario;
    public bool isVisible = false;

    private void Start()
    {
        inventario = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void PickupObject(string pickedObject)
    {
        if (pickedObject == "Vendaje") inventario.totalVendajes++;

        if (pickedObject == "Pila") inventario.totalBaterias++;
    }    

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
