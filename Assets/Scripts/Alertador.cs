using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alertador : MonoBehaviour
{

    
    Transform playerPos;
    Vector3 dir;
    [SerializeField] float distancia;
    [SerializeField] float rango;
    
    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        dir = playerPos.position - transform.position;
    }

    void Update()
    {
        distancia = Vector3.Distance(playerPos.position, transform.position);
        Detectar();
    }

    void Detectar()
    {

        if(distancia < rango)
        {
            GameObject[] cultistas = GameObject.FindGameObjectsWithTag("Enemigo");

            foreach (GameObject cultista in cultistas)
            {
                //Debug.Log("soy el enemigo numero " + cultista);
                cultista.SendMessage("Alertado");
            }        
        }
    }


}
