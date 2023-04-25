using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    public Animator anim;
    
    public GameObject player;                               //Referencia al objeto Jugador
    public float rango;                                     //Rango de deteccion del enemigo
    public bool detectado;
    
    RaycastHit rayInfo;
    NavMeshAgent _agent;
    
    public float distanceToPlayer;                          //Distancia entre el jugador y el enemigo                                                         
    public Vector3 direction;                               //Direccion del jugador para apuntarle

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        IsDetected();

        direction = player.transform.position - transform.position;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);    //actualizo distancia del jugador con el perseguidor para saber si esta en rango
        
        Chasing();
    }

    void Chasing()                                           //Funcion para el perseguidor
    {
        if (detectado)                       //Si el jugador esta en rango, perseguir
        {
            _agent.SetDestination(player.transform.position);
        }
    }

    void IsDetected()
    {
        if (Physics.Raycast(transform.position, direction, out rayInfo, rango))                                                                        //si el rayo devuelve una colision con el jugador, cambia a rojo, en alerta
        {
            if (rayInfo.collider.gameObject.tag == "Player")
            {
                if (detectado == false)
                {
                    detectado = true;
                }

                anim.SetBool("correr", true);
            }
            else
            {
                if (detectado == true)
                {
                    detectado = false;
                }
               
            }
        }
        else
        {
            detectado = false;
            anim.SetBool("correr", false);
        }
    }

  
}
