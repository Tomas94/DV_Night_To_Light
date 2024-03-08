using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireFly : Entity
{
    public Light lightToFollow;
    NavMeshAgent navM;
    Animator anim;
    Vector3 destiny;

    private void Start()
    {
        lightToFollow = GameManager.Instance.Flashlight._laserHitPoint;
        navM = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navM.speed = speed;
    }

    void Update()
    {
        FollowLight();
    }


    void FollowLight()
    {
        float distanceToLight = Vector3.Distance(lightToFollow.transform.position, transform.position);
        
        if (lightToFollow.enabled && distanceToLight <= _viewRadius)
        {
            destiny = lightToFollow.transform.position;
            navM.SetDestination(destiny);
            anim.SetBool("Caminando", true);
        }
        if(Vector3.Distance(destiny, transform.position) <= 1.5 && !lightToFollow.enabled)
        {
            anim.SetBool("Caminando", false);
        }
    }
   
    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}

