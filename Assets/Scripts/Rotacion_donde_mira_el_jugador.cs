using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion_donde_mira_el_jugador : MonoBehaviour { 

    Transform playerCamera;

    private void Start()
    {
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Transform>();
    }


    void Update()
    {
        // Obtén la posición horizontal del personaje y el objeto
        Vector3 characterPosition = playerCamera.position;
        Vector3 objectPosition = transform.position;
        characterPosition.y = 0f;
        objectPosition.y = 0f;

        // Calcula la dirección horizontal hacia el personaje
        Vector3 direction = characterPosition - objectPosition;
        direction.y = 0f;

        // Rota el objeto hacia la dirección horizontal sin cambiar la rotación vertical
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        }
    }
}

