using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]PlayerInventory inventario;

    private void Start()
    {
        inventario = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void PickupObject(string pickedObject)
    {
        if (pickedObject == "Venda") inventario.totalVendajes++;

        if (pickedObject == "Bateria") inventario.totalBaterias++;
    }



}
