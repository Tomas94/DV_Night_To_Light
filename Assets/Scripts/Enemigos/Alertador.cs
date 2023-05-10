using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alertador : MonoBehaviour
{

    
    Transform playerPos;
    Vector3 dir;
    [SerializeField] float distancia;
    [SerializeField] float rango;
    [SerializeField] GameObject[] cultistas;
    Color colorOriginal;


    void Start()
    {
        colorOriginal = GetComponent<Renderer>().material.color;
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        
        //GameObject[] cultistas = GameObject.FindGameObjectsWithTag("Enemigo");
    }

    void Update()
    {
        dir = playerPos.position - transform.position;
        Debug.DrawRay(transform.position, dir.normalized * rango,Color.red);
        distancia = Vector3.Distance(playerPos.position, transform.position);
        Detectar();
    }

    void Detectar()
    {

        if(distancia < rango)
        {
            foreach (GameObject cultista in cultistas)
            {              
                cultista.SendMessage("Alertado");
            }
            GetComponent<Renderer>().material.color = Color.red;
        }
        else GetComponent<Renderer>().material.color = colorOriginal;
    }
}
