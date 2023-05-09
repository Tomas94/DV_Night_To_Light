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
    [SerializeField] PlayerStatus pStatus;


    void Start()
    {
        _maxChargeTime = 5f;
        _maxBatteryCharge = _maxChargeTime * 60f;
        currentCharge = _maxBatteryCharge;
        flashlightLight.SetActive(false);
        laser.SetActive(false);
        pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
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
        if (Input.GetMouseButtonDown(0) && laser.activeSelf == false)
        {
            
            if (_flashlightActive == false)
            {
                flashlightLight.SetActive(true);
                pStatus.isNicto= false;
                _flashlightActive = true;
                uI.FlashLightState(true);
                Debug.Log("Tranquilo");
            }
            else
            {
                flashlightLight.SetActive(false);
                pStatus.isNicto = true;
                pStatus.IsFeared();
                _flashlightActive = false;
                uI.FlashLightState(false);
                Debug.Log("Asustado");
            }
            //AudioManager.Instance.PlaySFX("Encender_linterna");

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

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "LuzTrigger")
        {
            Debug.Log("En el trigger de nicto");
            pStatus.isNicto = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "LuzTrigger")
        {
            Debug.Log("saliendo del trigger de nicto");
            if (!_flashlightActive)pStatus.isNicto = true;
            if (_flashlightActive) pStatus.isNicto = false;
        }
        
    }
}
