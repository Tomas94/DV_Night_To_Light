using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SigueLuz : Entity
{
    public Light luz; //Referencia a la luz
    public float velocidad; //Velocidad de seguimiento del objeto
    NavMeshAgent navM;

    [SerializeField] float range;

    private void Start()
    {
        navM = GetComponent<NavMeshAgent>();
        currentHP = 1;
    }

    void Update()
    {
        FollowLight();
      //  OnDrawGizmos();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boton") other.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Boton") other.GetComponent<Renderer>().material.color = Color.red;
    }


    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void FollowLight()
    {
        float distanceToLight = Vector3.Distance(luz.transform.position, transform.position);
        if (luz.enabled && distanceToLight <= range) navM.SetDestination(luz.transform.position);
    }
}

