using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pickable_Items : Interactions
{
    [SerializeField] UI_Player player_UI;
    [SerializeField] Player_State playerInventory;
    [SerializeField] bool canPick = false;
    //[SerializeField] CharacterController player;
    public TextMeshProUGUI contenedorTexto;

    public bool testeo;

    [TextArea(5, 15)]
    public string texto;


    private void Awake()
    {      
        //diametroTrigger = 5f;
        CreateTrigger();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPick) AddObjectToInventory(transform.name);
        if (player_UI.textWindow.activeSelf && Input.GetMouseButtonDown(0))
        {
            player_UI.textWindow.SetActive(false);
            //player.enabled = true;
        }

        if (testeo)
        {
            playerInventory.isNicto = false;
            contenedorTexto.text = texto;
        }
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
            AudioManager.Instance.PlaySFX("Sonido_hoja");
            //player.enabled = false;
            player_UI.textWindow.SetActive(true);
            contenedorTexto.text = texto;
            return;
        }
         
        if (objeto == "Pila")
        {
            AudioManager.Instance.PlaySFX("Pickear");
            Debug.Log("Pickeaste una " + objeto);
            playerInventory.cantidadBaterias++;
            player_UI.BatteriesOnHold(playerInventory.cantidadBaterias);
        }

        if (objeto == "Vendaje")
        {
            AudioManager.Instance.PlaySFX("Pickear");
            Debug.Log("Pickeaste un " + objeto);
            playerInventory.cantidadVendajes++;
            player_UI.BandagesOnHold(playerInventory.cantidadVendajes);
        }
        
        InRange(false);
        Destroy(gameObject);
    }
}
