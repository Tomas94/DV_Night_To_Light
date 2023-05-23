using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PInteractions : Pickable
{

   [SerializeField] float distance = 5f;
    RaycastHit hit;

    void Update()
    {
        InteractableDetect();
    }

    void InteractableDetect()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.cyan);

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, LayerMask.GetMask("Interactable")))
        {
            Debug.Log(hit.transform.name);

            if (Input.GetKeyDown(KeyCode.E) && hit.transform.tag == "Pickable")
            {
                PickupObject(hit.transform.name);
                Destroy(hit.transform.gameObject);
            }
        }
    }



}
