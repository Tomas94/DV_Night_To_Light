using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Player : MonoBehaviour
{
    public GameObject canvasUI;
    [SerializeField]PlayerInventory inventario;
    [SerializeField] TextMeshProUGUI cantBaterias;
    [SerializeField] TextMeshProUGUI cantVendajes;

    private void Update()
    {
        BatteriesOnHold();
        CuresOnHold();
    }



    public void FlashLightState(bool encendida)
    {
        Transform flashLight = canvasUI.transform.GetChild(1);

        if (encendida)
        {
            //flashLight.GetChild(0).gameObject.SetActive(true);
            flashLight.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            //flashLight.GetChild(0).gameObject.SetActive(false);
            flashLight.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void BatteryState(float batPorcentaje, float maxCharge)
    {
        GameObject battery = canvasUI.transform.GetChild(2).gameObject;

        switch (batPorcentaje)
        {
            case float n when n >= (maxCharge * 0.75):
                DesactivarHijos(battery.transform.GetChild(4).gameObject, battery);
                break;

            case float n when n >= 50:
                DesactivarHijos(battery.transform.GetChild(3).gameObject, battery);
                break;

            case float n when n >= 25:
                DesactivarHijos(battery.transform.GetChild(2).gameObject, battery);
                break;

            case float n when n > 0:
                DesactivarHijos(battery.transform.GetChild(1).gameObject, battery);
                break;

            case float n when n <= 0:
                DesactivarHijos(battery.transform.GetChild(0).gameObject, battery);
                //battery.GetChild(1).gameObject.SetActive(false);
                break;
        }


    }

    public void CureState(int cantidad)
    {
        Transform vendajes = canvasUI.transform.GetChild(3);

        if(cantidad > 0)
        {
            vendajes.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            vendajes.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void BatteriesOnHold()
    {
        string bateriasInv = inventario.totalBaterias.ToString();
        cantBaterias.text = bateriasInv;
    }

    public void CuresOnHold()
    {
        string vendajesInv = inventario.totalVendajes.ToString();
        cantVendajes.text = vendajesInv;
    }

    public void LifeBar(int vidaActual)
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
