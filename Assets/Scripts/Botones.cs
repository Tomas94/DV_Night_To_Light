using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{
    public bool botonCorrecto;
    public Puzzle3 puzzleContador;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            if (botonCorrecto)
            {
                puzzleContador.puntos += 1;
                Debug.Log("Boton Correcto, +1 = " + puzzleContador.puntos);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") if (botonCorrecto)
            {
                puzzleContador.puntos -= 1; 
                Debug.Log("Boton Correcto, +1 = " + puzzleContador.puntos);
            }
    }
}
