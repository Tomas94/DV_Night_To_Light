using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamTittleScreen : MonoBehaviour
{
    public Transform[] waypoints;

    public float speed;

    int _indesWayPoints;

    private void Update()
    {
        if (_indesWayPoints >= waypoints.Length) { 
            _indesWayPoints = 0;
            return;
        }

        transform.position = Vector3.Lerp(transform.position, waypoints[_indesWayPoints].position, speed * Time.deltaTime);

        transform.forward = Vector3.Lerp(transform.forward, waypoints[_indesWayPoints].forward, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, waypoints[_indesWayPoints].position) <= 0.5f ) 
            _indesWayPoints++;  
    }
}
