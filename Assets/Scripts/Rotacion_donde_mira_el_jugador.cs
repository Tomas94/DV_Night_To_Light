using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion_donde_mira_el_jugador : MonoBehaviour
{
    public void Update()
    {
        transform.forward = Camera.main.transform.position - transform.position;
    }
}
