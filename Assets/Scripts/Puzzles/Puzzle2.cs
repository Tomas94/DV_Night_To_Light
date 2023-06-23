using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
{

    [SerializeField] Transform ObjetoAfectado;
    [SerializeField] GameObject camaraPuzzle;

    Camera mainCam;
    float tiempodemovimiento = 4f;
    float time = 0f;
    Quaternion rotacionInicial;
    Quaternion rotacionObjetivo;

    public int puntosNecesarios;
    public int puntos;

    void Start()
    {
        rotacionInicial = transform.rotation;
        rotacionObjetivo = Quaternion.Euler(0, 135, 0);

        mainCam = Camera.main;
        puntos = 0;
    }

    void Update()
    {
        if (puntos >= puntosNecesarios)
        {
            StartCoroutine(CompletedPuzzle2());
        }

    }

    IEnumerator CompletedPuzzle2()
    {
        camaraPuzzle.SetActive(true);
        mainCam.enabled = false;

        time += Time.deltaTime;
        float tiempoNormalizado = Mathf.Clamp01(time / tiempodemovimiento);
        Quaternion rotacionInterpolada = Quaternion.Lerp(rotacionInicial, rotacionObjetivo, tiempoNormalizado);
        ObjetoAfectado.rotation = rotacionInterpolada;
        yield return new WaitForSeconds(tiempodemovimiento);
        puntos = 0;
        mainCam.enabled = true;
        Destroy(gameObject);
    }
}
