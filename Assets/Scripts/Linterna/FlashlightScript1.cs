using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript1 : MonoBehaviour
{
    public GameObject flashlightLight;
    public GameObject laser;
    public UI_Player uI;
    private bool _flashlightActive = false;
    public float currentCharge;
    [SerializeField] float _maxChargeTime;
    public float _maxBatteryCharge;


    void Start()
    {
        _maxChargeTime = 5f;
        _maxBatteryCharge = _maxChargeTime * 60f;
        currentCharge = _maxBatteryCharge;
        flashlightLight.SetActive(false);
        laser.SetActive(false);
    }

    void Update()
    {
        FlashligthOnOff();
        AmplifiedLight();
        BatteryPercent();
        uI.BatteryState(currentCharge, _maxBatteryCharge);

    }

    void FlashligthOnOff()
    {
        if (Input.GetKeyDown(KeyCode.F) && laser.activeSelf == false)
        {
            if (_flashlightActive == false)
            {
                flashlightLight.SetActive(true);
                _flashlightActive = true;
                uI.FlashLightState(true);
            }
            else
            {
                flashlightLight.SetActive(false);
                _flashlightActive = false;
                uI.FlashLightState(false);
            }
        }
    }

    void AmplifiedLight()
    {
        if (Input.GetMouseButton(1) && _flashlightActive)
        {
            flashlightLight.SetActive(false);
            laser.SetActive(true);
        }
        else if (_flashlightActive)
        {
            flashlightLight.SetActive(true);
            laser.SetActive(false);
        }
    }

    void BatteryPercent()
    {
        if (_flashlightActive && currentCharge > 0)
        {
            currentCharge -= Time.deltaTime;
        }
    }
}
