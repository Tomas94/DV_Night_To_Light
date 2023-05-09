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
    
    RaycastHit hit;
    NavMeshAgent _agent;
    
    public float distanceToPlayer;                          //Distancia entre el jugador y el enemigo                                                         
    public Vector3 direction;                               //Direccion del jugador para apuntarle

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        direction = player.transform.position - transform.position;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);    //actualizo distancia del jugador con el perseguidor para saber si esta en rango
        IsDetected();
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
        Debug.DrawRay(transform.position, direction.normalized * rango, Color.cyan);

        if (Physics.Raycast(transform.position, direction, out hit, rango))                                                                        //si el rayo devuelve una colision con el jugador, cambia a rojo, en alerta
        {
            Debug.Log("Toco " + hit.transform.name);
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("tocó al jugador");
                if (detectado == false)
                {
                    detectado = true;
                    Debug.Log("detectó al player");
                }

                anim.SetBool("correr", true);
            }
            /*else
            {
                if (detectado == true)
                {
                    detectado = false;
                }
               
            }*/
        }
        else
        {
            Debug.Log("No Detectó nada");
            detectado = false;
            anim.SetBool("correr", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
