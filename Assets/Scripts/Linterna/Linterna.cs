using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Player_State player;
    public Carga_Bateria bateriaSlider;
    [SerializeField] Light light;
    [SerializeField] LineRenderer laserRenderer;
    GameObject linternalogo;


    [Header("Variables Linterna")]
    public float maxChargeTime;
    public float maxCharge;
    public float currentCharge;
    public bool isLightOn;
    public bool canLaserAttack;


    [Header("Variables Para Laser")]
    [SerializeField] float numOfReflections;
    Ray ray;
    RaycastHit hit;
    Vector3 rayDir;
    Light laserHitPoint;
    [SerializeField] LayerMask layer;
    [SerializeField] float laserLenght;

    private void Awake()
    {
        player.isNicto = !light.enabled;
        isLightOn = light.enabled;
    }
    void Start()
    {
        laserHitPoint = laserRenderer.GetComponentInChildren<Light>();
        linternalogo = GameObject.Find("Encendida");
        maxCharge = maxChargeTime * 60f;
        currentCharge = maxCharge;
        bateriaSlider.maxCharge = maxCharge;
        bateriaSlider.currentCharge = currentCharge;
        if (laserLenght <= 0) laserLenght = 50;
        canLaserAttack = true;
    }

    void Update()
    {
        LinternaOnOff();
        CargaRestante();
        //
        //ActivarLaser();
    }
    private void LateUpdate()
    {
        ActivarLaser();
    }

    void LinternaOnOff()
    {

        if (Input.GetKeyDown(KeyCode.F))
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
            AudioManager.Instance.PlaySFX("Encender_linterna");
        }

        player.isNicto = !isLightOn;
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
                LaserBehaviour();
            }
            else
            {
                light.enabled = true;
                laserRenderer.enabled = false;
                laserHitPoint.enabled = false;
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
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "Espejo")
                {
                    laserRenderer.positionCount += 1;
                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point);
                    remainLenght -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
                else if (hit.transform.tag == "Enemy")
                {
                    if (Input.GetMouseButtonDown(0)) StartCoroutine(LaserAttack(hit.transform.gameObject));
                    laserRenderer.positionCount += 1;
                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point);
                    laserHitPoint.enabled = true;
                    laserHitPoint.transform.position = hit.point;
                }
                else if (hit.transform.tag == "Boton")
                {
                    hit.transform.GetComponent<Puzzles>().puzzleCompleto = true;
                }
                else
                {
                    laserRenderer.positionCount += 1;
                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point);
                    laserHitPoint.enabled = true;
                    laserHitPoint.transform.position = hit.point;
                }
                
            }
            else
            {
                laserRenderer.positionCount += 1;
                laserRenderer.SetPosition(laserRenderer.positionCount - 1, ray.GetPoint(remainLenght));
            }
        }
    }

    IEnumerator LaserAttack(GameObject enemy)
    {
        if (canLaserAttack)
        {
            canLaserAttack = false;

            Debug.Log(enemy.name);
            List<Material> skinMaterials = new List<Material>();
            Renderer[] skinRenderer = enemy.GetComponentsInChildren<Renderer>();
            Chaser chaser = enemy.GetComponentInChildren<Chaser>();
            float isdead = chaser.currentHP;


            foreach (Renderer skin in skinRenderer)
            {
                Material[] materials = skin.materials;
                skinMaterials.AddRange(materials);
            }

            if (isdead == 3)
            {
                skinMaterials[0].color = new Color(0.6f, 0.6f, 0.6f, 1);
                skinMaterials[1].color = new Color(0.6f, 0.6f, 0.6f, 1);
                Debug.Log("enemigo dañano, le quedan 2 de hp");
            }
            else if (isdead == 2)
            {
                skinMaterials[0].color = new Color(0.3f, 0.3f, 0.3f, 1);
                skinMaterials[1].color = new Color(0.3f, 0.3f, 0.3f, 1);
                Debug.Log("enemigo dañano, le quedan 1 de hp");
            }
            else if (isdead == 1)
            {
                Debug.Log("enemigo dañano, le quedan 0 de hp");
                skinMaterials[0].color = Color.black;
                skinMaterials[1].color = Color.black;
            }

            chaser.TakeDamage();
            yield return new WaitForSeconds(2.5f);
            canLaserAttack = true;
        }
    }
}
