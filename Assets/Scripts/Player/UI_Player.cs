using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Player : MonoBehaviour
{
    public GameObject canvasUI;
    [SerializeField] PlayerStatus pStatus;
    [SerializeField] TextMeshProUGUI cantBaterias;
    [SerializeField] TextMeshProUGUI cantVendajes;

    private void Start()
    {
        pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    public void FlashLightState(bool encendida)
    {
        Transform flashLightIcon = canvasUI.transform.GetChild(1);

        if (encendida)
        {
            flashLightIcon.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            flashLightIcon.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void BatteryState(float batPorcentaje, float maxCharge)
    {
        GameObject batteryIcon = canvasUI.transform.GetChild(2).gameObject;

        switch (batPorcentaje)
        {
            case float n when n >= (maxCharge * 0.75):
                DesactivarHijos(batteryIcon.transform.GetChild(4).gameObject, batteryIcon);
                break;

            case float n when n >= (maxCharge * 0.5):
                DesactivarHijos(batteryIcon.transform.GetChild(3).gameObject, batteryIcon);
                break;

            case float n when n >= (maxCharge * 0.25):
                DesactivarHijos(batteryIcon.transform.GetChild(2).gameObject, batteryIcon);
                break;

            case float n when n > 0:
                DesactivarHijos(batteryIcon.transform.GetChild(1).gameObject, batteryIcon);
                break;

            case float n when n <= 0:
                DesactivarHijos(batteryIcon.transform.GetChild(0).gameObject, batteryIcon);
                break;
        }


    }

    public void BandageState(int cantidad)
    {
        Transform bandageIcon = canvasUI.transform.GetChild(3);

        if(cantidad > 0)
        {
            bandageIcon.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            bandageIcon.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void LifeBarState(int vidaActual)
    {
        GameObject life = canvasUI.transform.GetChild(5).gameObject;

        switch (vidaActual)
        {
            case 3:
                DesactivarHijos(life.transform.GetChild(2).gameObject, life);
                break;
            case 2:
                DesactivarHijos(life.transform.GetChild(1).gameObject, life);
                break;
            case 1:
                DesactivarHijos(life.transform.GetChild(0).gameObject, life);
                break;
            case 0:
                life.transform.GetChild(0).gameObject.SetActive(false);
                break;
        }

    }

    public void BatteriesOnHold(int bats)
    {
        string bateriasInv = bats.ToString();
        cantBaterias.text = bateriasInv;
    }

    public void BandagesOnHold(int bands)
    {
        string vendajesInv = bands.ToString();
        cantVendajes.text = vendajesInv;
    }

    void DesactivarHijos(GameObject gO_Index, GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject hijo = parent.transform.GetChild(i).gameObject;

            if (hijo != gO_Index)
            {
                hijo.SetActive(false);
            }
            else
            {
                hijo.SetActive(true);
            }
        }
    }
}
