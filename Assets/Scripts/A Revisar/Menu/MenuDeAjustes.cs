using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuDeAjustes : MonoBehaviour
{
   
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    /*public void CambiarCalidad(int index)
    {
        QualitySettings.GetQualityLevel(index);
    }
    */
}
