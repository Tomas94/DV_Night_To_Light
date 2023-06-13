using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    [SerializeField] Player_State player;
    [SerializeField] Carga_Bateria bateriaSlider;
    [SerializeField] new Light light;
    GameObject linternalogo;
    public float maxChargeTime;
    public float maxCharge;
    public float currentCharge;
    public bool isLightOn;

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


}
