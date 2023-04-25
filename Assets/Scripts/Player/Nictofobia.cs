using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nictofobia : MonoBehaviour
{
    public bool nictofobia;
    [SerializeField] GameObject linterna;


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "LuzTrigger")
        {
            nictofobia = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nictofobia = true;
    }
}
