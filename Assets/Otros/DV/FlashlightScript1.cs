using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript1 : MonoBehaviour
{
    public GameObject flashlightLight;
    public GameObject laser;
    private bool _flashlightActive = false;

    void Start()
    {
        flashlightLight.gameObject.SetActive(false);
        laser.gameObject.SetActive(false);
    }

    void Update()
    {
        FlashligthOnOff();
        AmplifiedLight();
    }

    void FlashligthOnOff()
    {
        if (Input.GetKeyDown(KeyCode.F) && laser.activeSelf == false)
        {
            if (_flashlightActive == false)
            {
                flashlightLight.gameObject.SetActive(true);
                _flashlightActive = true;            
            }
            else
            {
                flashlightLight.gameObject.SetActive(false);
                _flashlightActive = false;
            }
        }
    }

    void AmplifiedLight()
    {
        if(Input.GetMouseButton(1) && _flashlightActive)
        {
            flashlightLight.gameObject.SetActive(false);
            laser.gameObject.SetActive(true);
        }
        else if (_flashlightActive)
        {
            flashlightLight.gameObject.SetActive(true);
            laser.gameObject.SetActive(false);
        }
    }
}
