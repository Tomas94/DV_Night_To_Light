using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptLights : MonoBehaviour
{
    Transform _cam;

    public float minDist;

    [Header("Referencia automática")] 
    public Light light;

    void Start()
    {
        _cam = Camera.main.transform;

        light = GetComponent<Light>();
    }

    private void Update()
    {
        if(Vector3.Distance(_cam.position, transform.position) <= minDist && !light.enabled)
            light.enabled = true;
        else if(Vector3.Distance(_cam.position, transform.position) > minDist && light.enabled)
            light.enabled = false;
    }
}
