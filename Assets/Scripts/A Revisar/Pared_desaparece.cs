using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared_desaparece : MonoBehaviour
{
    public Activada esfera1;

    private void Update()
    {
        if (esfera1.activado)
        {
            Destroy(gameObject);
        }
    }
}
