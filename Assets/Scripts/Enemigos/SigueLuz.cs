using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SigueLuz : MonoBehaviour
{
    public GameObject luz; //Referencia a la luz
    public float velocidad; //Velocidad de seguimiento del objeto
    NavMeshAgent navM;

    [SerializeField] float range;

    private void Start()
    {
        navM = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FollowLight();
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
        if (luz.gameObject.activeSelf && distanceToLight <= range) navM.SetDestination(luz.transform.position);
    }
}

