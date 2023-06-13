using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carga_Bateria : MonoBehaviour
{
    [SerializeField] RectTransform slider;
    public float maxCharge;
    public float currentCharge;

    void Update()
    {
        CargaRestante();
    }

    void CargaRestante()
    {
        if (currentCharge > 0)
        {
            slider.localScale = Vector3.one * ((100 / maxCharge) * (currentCharge/100));
        }
    }

}
