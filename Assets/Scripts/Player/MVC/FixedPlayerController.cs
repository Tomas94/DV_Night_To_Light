using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlayerController : MonoBehaviour
{
    private FixedPlayerModel _model;
    Vector3 _dir = new Vector3();

    private void Awake()
    {
        _model = GetComponent<FixedPlayerModel>();
    }

    public void ListenFixedKeys()
    {
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxis("Vertical");

        _model.Movement(_dir);
    }

    public void UpdateKeys()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            _model.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //model.Cambiar entre UV y normal
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //model.Prender o Apagar Linterna
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //model. Usar pastilla vida
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //model. Usar pastilla Stamina
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //model. Usar pastilla vision.
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //model. Correr
        }

        if (Input.GetMouseButton(1))
        {
            //model. Apuntar con linterna

            if (Input.GetMouseButton(0))
            {
                //model. Destello.
            }
        }


    }


}
