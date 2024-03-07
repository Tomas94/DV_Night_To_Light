using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVObjects : MonoBehaviour
{
    [SerializeField] GameObject _hidenObject;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hizo trigger con " + other.name);
        Flashlight uvLight = other.GetComponentInParent<Flashlight>();
        if (uvLight == null) Debug.Log("nada");
        if (uvLight != null)
        {
            Debug.Log("se encontro una linterna");
            if(uvLight.isUVActive())_hidenObject.SetActive(true);
        }
    }
}
