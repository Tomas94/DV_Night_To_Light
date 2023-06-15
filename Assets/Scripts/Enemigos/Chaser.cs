using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    Transform player;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask structures; 
    [SerializeField] NavMeshAgent chaserNM;
    RaycastHit hit;
    
    [SerializeField] float _distanciaPlayer;
    public int rangoVision;
    public int rangoAtaque;
    Vector3 _direccion;
   
    [SerializeField] bool _detectado;
    [SerializeField] bool _canAttack;

    void Start()
    {       
        player = GameObject.Find("DetectPoint").GetComponent<Transform>();
        _canAttack = true;
    }

    void Update()
    {
        _direccion = player.position - transform.position;
        _distanciaPlayer = Vector3.Distance (player.position, transform.position);
        Debug.DrawRay(transform.position, _direccion.normalized * rangoVision);

        DetectPlayer();
        CultistState();
    }

    void DetectPlayer()
    {
        if (Physics.Raycast(transform.position, _direccion, out hit, rangoVision, structures) || _distanciaPlayer >= rangoVision)
        {
            _detectado = false;
            Debug.Log("Perdido");
        }
        else if(Physics.Raycast(transform.position, _direccion, out hit, rangoVision, LayerMask.GetMask("Player")))
        {
            Debug.Log("Detectado");
            _detectado = true;
            anim.SetBool("EnRango", true);
        }
    }

    void CultistState()
    {
        if (_detectado)
        {
            if (_distanciaPlayer <= rangoAtaque)
            {
                chaserNM.isStopped = true;
                Debug.Log("En Rango de Ataque");

                if (_canAttack)
                {
                    StartCoroutine(AttackPlayer());
                }

                anim.SetBool("AtackRange", true);
                anim.SetBool("EnRango", false);
            }
            else
            {
                Alertado();
                Debug.Log("En Rango de Persecucion");
           }
        }
        else
        {
            chaserNM.isStopped = true;
            Debug.Log("Fuera de Rango");
            anim.SetBool("EnRango", false);
        }
    }

    public IEnumerator AttackPlayer()
    {
        _canAttack = false;
        yield return new WaitForSeconds(2);
        //PlayerStatus player = GameObject.Find("Player").GetComponent<PlayerStatus>();
        //if (_distanciaPlayer <= rangoAtaque) player.TakeDamage();
        yield return new WaitForSeconds(1);
        _canAttack = true;

    }

    public void Alertado()
    {
        chaserNM.isStopped = false;
        _detectado = true;
        anim.SetBool("AtackRange", false);
        anim.SetBool("EnRango", true);
        chaserNM.SetDestination(player.position);
    }


}
