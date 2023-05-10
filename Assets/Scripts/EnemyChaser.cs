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
    public int rangoAtaque;
    Vector3 _direccion;
    [SerializeField] LayerMask structures; 
    NavMeshAgent chaserNM;
    
    RaycastHit hit;
    [SerializeField] bool _detectado;
    [SerializeField] bool _canAttack;

    void Start()
    {
        player = GameObject.Find("DetectPoint").GetComponent<Transform>();
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
        if (Physics.Raycast(transform.position, _direccion, out hit, rangoVision, structures) || _distanciaPlayer >= rangoVision)
        {
            _detectado = false;
            return;
        }
        else if(Physics.Raycast(transform.position, _direccion, out hit, rangoVision, LayerMask.GetMask("Player")))
        {
            Debug.Log("Detectado");
            _detectado = true;
            anim.SetBool("EnRango", true);
        }
    }

    void ChasePlayer()
    {
        if (_detectado)
        {
            if (_distanciaPlayer <= rangoAtaque)
            {
                //anim.SetBool("EnRango", false);
                anim.SetBool("AtackRange", true);
            }
            else
            {
                //anim.SetBool("EnRango", true);
                anim.SetBool("AtackRange", false);
                chaserNM.SetDestination(player.position);
            }
        }
        else
        {
            anim.SetBool("EnRango", false);
        }
    }

    void AttackPlayer()
    {

    }


}
