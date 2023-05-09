using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : MonoBehaviour
{
    Transform player;
    [SerializeField] Animator anim;


    [SerializeField] float _distanciaPlayer;
    public int rangoVision;
    Vector3 _direccion;
    NavMeshAgent chaserNM;
    
    RaycastHit hit;
    [SerializeField] bool _detectado;


    void Start()
    {
        player = GameObject.Find("PlayerCamera").GetComponent<Transform>();
        chaserNM = GameObject.Find("Cultist").GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _direccion = player.position - transform.position;
        _distanciaPlayer = Vector3.Distance (player.position, transform.position);
        Debug.DrawRay(transform.position, _direccion.normalized * rangoVision);

        DetectPlayer();
        ChasePlayer();
    }

    void DetectPlayer()
    {
        if (Physics.Raycast(transform.position, _direccion, out hit, rangoVision, LayerMask.GetMask("Player")))
        {
            Debug.Log(hit.transform.name);
            _detectado = true;
        }
        else
        {
            _detectado = false;
            anim.SetBool("EnRango", false);
        }
    }

    void ChasePlayer()
    {
        if (_detectado && _distanciaPlayer <= chaserNM.stoppingDistance)
        {
            anim.SetBool("EnRango", false);
            return;
        }
        if (_detectado)
        {
            anim.SetBool("EnRango", true);
            chaserNM.SetDestination(player.position);
        }      
    }



}
