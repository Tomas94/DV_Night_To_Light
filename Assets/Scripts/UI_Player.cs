using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player : MonoBehaviour
{
    public GameObject canvasUI;

    public void FlashLightState(bool encendida)
    {
        Transform flashLight = canvasUI.transform.GetChild(1);

        if (encendida)
        {
            flashLight.GetChild(0).gameObject.SetActive(true);
            flashLight.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            flashLight.GetChild(0).gameObject.SetActive(false);
            flashLight.GetChild(1).gameObject.SetActive(true);
        }
    }
}
