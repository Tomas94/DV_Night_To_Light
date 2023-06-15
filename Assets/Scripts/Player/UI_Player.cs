using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Player : MonoBehaviour
{

    [Header("Referencias Iconos")]
    [SerializeField] GameObject canvasUI;
    [SerializeField] GameObject vendajeIcon;
    [SerializeField] GameObject hpIcon;

    [Header("Referencias Textos")]
    [SerializeField] TextMeshProUGUI cantBateriasTMP;
    [SerializeField] TextMeshProUGUI cantVendajesTMP;

    #region script outdated
    /* public void FlashLightOnOff(bool encendida)
     {
         if (encendida)
         {
             flashlightStateIcon.transform.GetChild(1).gameObject.SetActive(false);
         }
         else
         {
             flashlightStateIcon.transform.GetChild(1).gameObject.SetActive(true);
         }
     }*/

    /* public void BatteryState(float batPorcentaje, float maxCharge)
     {
         switch (batPorcentaje)
         {
             case float n when n >= (maxCharge * 0.75):
                 DesactivarHijos(bateriaIcon.transform.GetChild(4).gameObject, bateriaIcon);
                 break;

             case float n when n >= (maxCharge * 0.5):
                 DesactivarHijos(bateriaIcon.transform.GetChild(3).gameObject, bateriaIcon);
                 break;

             case float n when n >= (maxCharge * 0.25):
                 DesactivarHijos(bateriaIcon.transform.GetChild(2).gameObject, bateriaIcon);
                 break;

             case float n when n > 0:
                 DesactivarHijos(bateriaIcon.transform.GetChild(1).gameObject, bateriaIcon);
                 break;

             case float n when n <= 0:
                 DesactivarHijos(bateriaIcon.transform.GetChild(0).gameObject, bateriaIcon);
                 break;
         }
     }*/

    /*public void BandageState(int cantidad)
    {
        if (cantidad > 0)
        {
            vendajeIcon.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            vendajeIcon.transform.GetChild(1).gameObject.SetActive(true);
        }
    }*/

    #endregion

    public void LifeBarState(int vidaActual)
    {
        switch (vidaActual)
        {
            case 3:
                DesactivarHijos(hpIcon.transform.GetChild(3).gameObject, hpIcon);
                break;
            case 2:
                DesactivarHijos(hpIcon.transform.GetChild(2).gameObject, hpIcon);
                break;
            case 1:
                DesactivarHijos(hpIcon.transform.GetChild(1).gameObject, hpIcon);
                break;
            case 0:
                hpIcon.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }

    public void BatteriesOnHold(int batts)
    {
        string bateriasInv = batts.ToString();

        cantBateriasTMP.text = bateriasInv;

        cantBateriasTMP.text = "x " + bateriasInv;
    }

    public void BandagesOnHold(int bands)
    {
        string vendajesInv = bands.ToString();
        cantVendajesTMP.text = "x " + vendajesInv;
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
