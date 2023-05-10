using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [SerializeField] bool trapactive;
    [SerializeField] GameObject fire;
    Color colorOriginal;

    private void Start()
    {
        colorOriginal = GetComponent<Renderer>().material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Entro en Trigger");
            StartCoroutine(ActivarTrampa());
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Player Esta en Trigger");
        if (other.tag == "Player" && trapactive)
        {        
            PlayerStatus pStatus = other.GetComponent<PlayerStatus>();
            pStatus.TakeDamage();
            trapactive = false;
        }
    }
        

    IEnumerator ActivarTrampa()
    { 
        yield return new WaitForSeconds(2);
        fire.SetActive(true);
        //GetComponent<Renderer>().material.color = Color.red;
        trapactive = true;
        yield return new WaitForSeconds(3);
        //GetComponent<Renderer>().material.color = colorOriginal;
        fire.SetActive(false);
        trapactive = false;
    }
}
