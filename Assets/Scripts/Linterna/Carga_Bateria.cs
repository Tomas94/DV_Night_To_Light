using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carga_Bateria : MonoBehaviour
{
    [SerializeField] RectTransform slider;
    public float maxCharge;
    public float currentCharge;
    public float maxChargeTime;

    void Awake()
    {
        maxCharge = maxChargeTime * 60f;
        currentCharge = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        CargaRestante();
    }

    void CargaRestante()
    {
        if (currentCharge > 0)
        {
            currentCharge -= Time.deltaTime;
            slider.localScale = Vector3.one * ((100 / maxCharge) * (currentCharge/100));
        }
    }

}
