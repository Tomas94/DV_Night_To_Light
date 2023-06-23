using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour
{

    [SerializeField] Transform ObjetoAfectado;
    [SerializeField] GameObject camaraPuzzle;

    Camera mainCam;
    float tiempodemovimiento = 4f;


    public int puntosNecesarios;
    public int puntos;

    void Start()
    {
        mainCam = Camera.main;
        puntos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (puntos >= puntosNecesarios)
        {
            StartCoroutine(CompletedPuzzle3());
        }

    }

    IEnumerator CompletedPuzzle3()
    {
        camaraPuzzle.SetActive(true);
        mainCam.enabled = false;    
        float time = Time.deltaTime;
        ObjetoAfectado.position = new Vector3(ObjetoAfectado.transform.position.x, Mathf.Lerp(ObjetoAfectado.transform.position.y, ObjetoAfectado.transform.position.y - 50, time / tiempodemovimiento), ObjetoAfectado.transform.position.z);
        yield return new WaitForSeconds(tiempodemovimiento);
        puntos = 0;
        mainCam.enabled = true;
        Destroy(gameObject);
    }
}
