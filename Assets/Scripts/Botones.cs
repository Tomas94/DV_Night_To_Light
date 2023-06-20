using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{
    public bool botonCorrecto;
    public Puzzle3 puzzleContador;
    Renderer renderer;
    public Material pisadoCorrecto;
    public Material pisadoIncorrecto;
    public Material sinPisar;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            if (botonCorrecto)
            {
                renderer.material = pisadoCorrecto;
                puzzleContador.puntos += 1;
                Debug.Log("Boton Correcto, +1 = " + puzzleContador.puntos);
            }
            else renderer.material = pisadoIncorrecto;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            renderer.material = sinPisar;

            if (botonCorrecto)
            {
                puzzleContador.puntos -= 1;

                Debug.Log("Boton Correcto, +1 = " + puzzleContador.puntos);
            }
        }
    }
}
