using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesLab : MonoBehaviour
{
    [SerializeField] Puzzle2 puzzle2;
    public bool canPress;
    [SerializeField] GameObject player;
    float distance;
    bool activado;

    private void Start()
    {
        activado = false;
        canPress = true;
    }

    private void Update()
    {

        distance = Vector3.Distance(player.transform.position, transform.position);


        if(Input.GetKeyDown(KeyCode.E) && canPress)
        {
            puzzle2.puntos += 1;
            GetComponentInChildren<AudioSource>().Stop();
            activado = true;
        }
        if (distance <= 5 && !activado) canPress = true;
        else canPress = false;
    }

   

    IEnumerator PulsarBoton()
    { 
        puzzle2.puntos+=1;
        yield return null;
    }

}
