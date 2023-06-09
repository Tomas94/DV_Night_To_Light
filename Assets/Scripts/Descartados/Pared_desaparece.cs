using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared_desaparece : MonoBehaviour
{
    public Activada espejo_roto;

    private void Update()
    {
        if (espejo_roto.activado)
        {
            Destroy(gameObject);
        }
    }
}
