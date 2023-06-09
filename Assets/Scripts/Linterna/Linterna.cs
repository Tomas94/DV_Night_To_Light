using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] Player_State player;
    [SerializeField] new Light light;
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
        maxCharge = maxChargeTime * 60f;
        currentCharge = maxCharge;
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
                isLightOn = true;
                light.enabled = true;
            }
            else
            {
                isLightOn = false;
                light.enabled = false;
            }
            
            player.isNicto = !isLightOn;
        }
    }

    void CargaRestante()
    {
        if (isLightOn && currentCharge > 0) currentCharge -= Time.deltaTime;
    }



}
