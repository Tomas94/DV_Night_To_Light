using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Outline))]

public class Interactions : MonoBehaviour //Esta clase va a ser el padre de los objetos pickeables e interactuables
{
    public float diametroTrigger = 1f;
    Outline outline;
    GameObject canvasPick;
    
    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        canvasPick = transform.GetChild(0).gameObject;
    }



    public void CreateTrigger()
    {
        SphereCollider sphereTrigger = gameObject.AddComponent<SphereCollider>();
        sphereTrigger.isTrigger = true;
        sphereTrigger.radius = diametroTrigger / 2f;

        Debug.Log("el radio del trigger es " + sphereTrigger.radius);
    }

    public void InRange(bool display)
    {


        if (display)
        {
            outline.enabled = display;
            canvasPick.SetActive(true);

        }
        else
        {
            outline.enabled = display;
            canvasPick.SetActive(false);
        }



        //Codigo que muestre en el UI mensaje para agarrar o interactuar con un objeto
    }

    void OnValidate()
    {
        if (Application.isPlaying)
        {
            SphereCollider collider = GetComponent<SphereCollider>();  // Obtener el collider esférico del objeto hijo

            if (collider != null)
            {
                collider.radius = diametroTrigger / 2f;                // Actualizar el diámetro del collider
            }
        }
    }

}
