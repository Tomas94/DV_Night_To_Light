using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Player_State player;
    [SerializeField] Carga_Bateria bateriaSlider;
    [SerializeField] new Light light;
    [SerializeField] new LineRenderer laserRenderer;
    GameObject linternalogo;
    
    [Header("Variables Linterna")]
    public float maxChargeTime;
    public float maxCharge;
    public float currentCharge;
    public bool isLightOn;

    [Header("Variables Para Laser")]
    Ray ray;
    RaycastHit hit;
    Vector3 rayDir;
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

        if (Physics.Raycast(ray.origin, ray.direction, out hit , remainLenght))
        {
            laserRenderer.positionCount += 1;
            laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point);
        }
    }




}
