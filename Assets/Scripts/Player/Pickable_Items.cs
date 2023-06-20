using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Items : Interactions
{
    [SerializeField] UI_Player player_UI;
    [SerializeField] Player_State playerInventory;
    [SerializeField] bool canPick = false;

    private void Awake()
    {
        diametroTrigger = 5f;
        CreateTrigger();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPick) AddObjectToInventory(transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player_State player = other.GetComponent<Player_State>();
        
        if(player != null)
        {
            Debug.Log("en contacto con player");
            canPick = true;
            InRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player_State player = other.GetComponent<Player_State>();

        if (player != null)
        {
            Debug.Log("lejos del player");
            canPick = false;
            InRange(false);
        }
    }

    void AddObjectToInventory(string objeto)
    {
        if(objeto == "Nota")
        {
            return;
        }
         
        if (objeto == "Pila")
        {
            Debug.Log("Pickeaste una " + objeto);
            playerInventory.cantidadBaterias++;
            player_UI.BatteriesOnHold(playerInventory.cantidadBaterias);
        }

        if (objeto == "Vendaje")
        {
            Debug.Log("Pickeaste un " + objeto);
            playerInventory.cantidadVendajes++;
            player_UI.BandagesOnHold(playerInventory.cantidadVendajes);
        }
        AudioManager.Instance.PlaySFX("Pickear");

        InRange(false);
        Destroy(gameObject);
    }
}
