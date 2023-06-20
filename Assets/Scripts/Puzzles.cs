using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzles : MonoBehaviour
{
    [SerializeField] Transform ObjetoAfectado;
    public bool puzzleCompleto = false;
    [SerializeField] Camera camaraPuzzle;

    Camera mainCam;
    float tiempodemovimiento = 2f;

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        //PuzzleCompletado();
        if (puzzleCompleto) StartCoroutine(Completed());

    }

    void PuzzleCompletado()
    {
        if (puzzleCompleto)
        {
            puzzleCompleto = false;
            //  ObjetoAfectado = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y - 11 , 1), transform.position.z);
        }
    }

    IEnumerator Completed()
    {
        mainCam.enabled = false;
        float time = Time.deltaTime;
        ObjetoAfectado.position = new Vector3(ObjetoAfectado.transform.position.x, Mathf.Lerp(ObjetoAfectado.transform.position.y, ObjetoAfectado.transform.position.y - 11, time / tiempodemovimiento), ObjetoAfectado.transform.position.z);
        yield return new WaitForSeconds(tiempodemovimiento);
        puzzleCompleto = false;
        mainCam.enabled = true;
        Destroy(gameObject);
    }
}
