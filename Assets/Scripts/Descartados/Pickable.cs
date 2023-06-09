using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    PlayerStatus pStatus;

    private void Start()
    {
        pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    public void PickupObject(string pickedObject)
    {
        Debug.Log("Pickeaste un " + pickedObject);

        if (pickedObject == "Pila")
        {
            Debug.Log("Pickeaste una " + pickedObject);
            pStatus.status.batteries++;
            pStatus._uI.BatteriesOnHold(pStatus.status.batteries);
        }
        
        if (pickedObject == "Vendaje")
        {
            Debug.Log("Pickeaste un " + pickedObject);
            pStatus.status.bandages++;
            pStatus._uI.BandagesOnHold(pStatus.status.bandages);
        }
    }
}
