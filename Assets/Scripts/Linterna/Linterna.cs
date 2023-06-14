using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Player_State player;
    [SerializeField] Carga_Bateria bateriaSlider;
    [SerializeField] Light light;
    [SerializeField] LineRenderer laserRenderer;
    GameObject linternalogo;
    
    [Header("Variables Linterna")]
    public float maxChargeTime;
    public float maxCharge;
    public float currentCharge;
    public bool isLightOn;

    [Header("Variables Para Laser")]
    [SerializeField] float numOfReflections;
    Ray ray;
    RaycastHit hit;
    Vector3 rayDir;
    [SerializeField] LayerMask layer;
    [SerializeField] float laserLenght;

    private void Awake()
    {
        player.isNicto = !light.enabled;
        isLightOn = light.enabled;
    }
    void Start()
    {
        linternalogo = GameObject.Find("Encendida");
        maxCharge = maxChargeTime * 60f;
        currentCharge = maxCharge;
        bateriaSlider.maxCharge = maxCharge;
        bateriaSlider.currentCharge = currentCharge;
        if (laserLenght <=0) laserLenght = 50;
    }

    void Update()
    {
        LinternaOnOff();
        CargaRestante();
        ActivarLaser();
        LaserBehaviour();
    }

    void LinternaOnOff()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (!isLightOn)
            {
                linternalogo.SetActive(true);
                isLightOn = true;
                light.enabled = true;
            }
            else
            {
                linternalogo.SetActive(false);
                isLightOn = false;
                light.enabled = false;
            }

            player.isNicto = !isLightOn;
        }
        linternalogo.SetActive(isLightOn ? true : false);
    }

    void CargaRestante()
    {
        if (isLightOn && currentCharge > 0)
        {
            currentCharge -= Time.deltaTime;
            bateriaSlider.currentCharge = currentCharge;
        }
        else
        {
            linternalogo.SetActive(false);
            light.enabled = false;
            isLightOn = false;
        }
    }

    void ActivarLaser()
    {
        if (isLightOn)
        {
            if (Input.GetMouseButton(1))
            {
                light.enabled = false;
                laserRenderer.enabled = true;
            }
            else
            {
                light.enabled = true;
                laserRenderer.enabled = false;
            }
        }
        else
        {
            light.enabled = false;
            laserRenderer.enabled = false;
        }


    }

    void LaserBehaviour()
    {
        float remainLenght = laserLenght;
        ray = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);
        laserRenderer.positionCount = 1;
        laserRenderer.SetPosition(0, transform.position);

        for (int i = 0; i < numOfReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainLenght, layer))
            {
                if (hit.transform.tag == "Espejo")
                {
                    laserRenderer.positionCount += 1;
                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point);

                    remainLenght -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
                else
                {
                    laserRenderer.positionCount += 1;
                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point);
                    return;
                }
            }
            else
            {
                laserRenderer.positionCount += 1;
                laserRenderer.SetPosition(laserRenderer.positionCount - 1, ray.GetPoint(remainLenght));
            }
        }      
    }
}
