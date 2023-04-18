using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    [Header("Settings")]
    //public LayerMask layerMask;
    public float defaultLength = 50;
    public int numOfReflections = 2;

    private LineRenderer _lineRenderer;
    private RaycastHit hit;

    private Ray ray;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //NormalLaser();
        ReflectLaser();
    }

    public void ReflectLaser()
    {
        ray = new Ray(transform.position, transform.forward);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainLenght = defaultLength;

        for (int i = 0; i < numOfReflections; i++) 
        {
            // Does the ray intersect any objects
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainLenght, LayerMask.GetMask("Obstacle")))
            {               
                if(hit.transform.tag == "Mirror") { 

                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                    remainLenght -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));}
                else if(hit.transform.tag == "Button")
                {
                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                    hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLenght));
            }
        }
    }

    public void NormalLaser()
    {
        _lineRenderer.SetPosition(0, transform.position);

        // Does the ray intersect any objects
        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, LayerMask.GetMask("Obstacle")))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }
    }
}
