using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SigueLuz : MonoBehaviour
{
    public GameObject luz; //Referencia a la luz
    public float velocidad; //Velocidad de seguimiento del objeto
    NavMeshAgent navM;

    private void Start()
    {
        navM = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (luz.gameObject.activeSelf)  navM.SetDestination(luz.transform.position);
    }
}

