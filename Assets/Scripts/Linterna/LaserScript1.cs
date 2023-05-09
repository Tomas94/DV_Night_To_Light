using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript1 : MonoBehaviour
{
    [Header("Settings")]
    //public LayerMask layerMask;
    public float defaultLength = 50;
    public int numOfReflections = 2;

    private LineRenderer _lineRenderer;
    private RaycastHit hit;

    //int screenWidth= Screen.width;
    //int screenHeight= Screen.height;
    //Camera mainCamera = Camera.main;


 


    //public Activada esfera1;

    private Ray ray;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        //Vector3 screenCenter = new Vector3(screenWidth / 2f, screenHeight / 2f, mainCamera.nearClipPlane);
        //Vector3 worldCenter = mainCamera.ScreenToWorldPoint(screenCenter);
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
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainLenght, LayerMask.GetMask("Interactable")))
            {
                if (hit.transform.tag == "Espejo")
                {
                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                    remainLenght -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
                else if (hit.transform.tag == "Boton")
                {
                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
                    AudioManager.Instance.PlaySFX("Cuervos");
                    if (hit.transform.name == "Esfera1")
                    {
                        //esfera1.Activado();
                        //Destroy(esfera1.gameObject);
                    }
                    return;
                }
            }      
        }
        if (Physics.Raycast(ray.origin, ray.direction, out hit, remainLenght, LayerMask.GetMask("Surface","Ground")))
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
            return;
        }
        else
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLenght));
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
