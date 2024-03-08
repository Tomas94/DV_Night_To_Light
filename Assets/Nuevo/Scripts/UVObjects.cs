using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVObjects : MonoBehaviour
{
    [SerializeField] GameObject _hidenObject;

    private void OnTriggerStay(Collider other)
    {
        Flashlight uvLight = other.GetComponentInParent<Flashlight>();
        if (uvLight != null)
        {
            if (uvLight.isUVActive()) _hidenObject.SetActive(true);
            else
            {
                _hidenObject.SetActive(false);
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        _hidenObject.SetActive(false);
    }
}
