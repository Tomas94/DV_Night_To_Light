using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerLogic _model;

    private void Awake()
    {
        _model = GetComponent<PlayerLogic>();
    }

    public void UpdateKeys()
    {
        Movement();                                                                     //Movimiento Básico

        if (Input.GetKey(KeyCode.LeftShift)) _model.Run(true);                          //Correr

        if (Input.GetKeyUp(KeyCode.LeftShift)) _model.Run(false);                       //Dejar de Correr

        if (Input.GetKeyDown(KeyCode.R)) _model.UseItem(Consumables.battery);           //Recargar Bateria

        if (Input.GetKeyDown(KeyCode.Alpha1)) _model.UseItem(Consumables.healthPill);   // Usar pastilla vida

        if (Input.GetKeyDown(KeyCode.Alpha2)) _model.UseItem(Consumables.staminaPill);  // Usar pastilla Stamina

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //model. Usar pastilla vision.
        }

        if (Input.GetKeyDown(KeyCode.E)) _model.Interact();                             //Interactuar

        if (Input.GetKeyDown(KeyCode.F)) _model.FlashlightClick();                      //Prender o Apagar Linterna

        if (Input.GetMouseButton(1))                                                    //Prender Laser
        {
            _model.FlashlightFocusedLight(true);
        }

        if (Input.GetMouseButtonUp(1)) _model.FlashlightFocusedLight(false);            //Apagar Laser

        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Q)) _model.SwitchLight();
    }
    
    
    public void Movement()
    {
        Vector3 direction;
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if (_model._chController != null)
        {
            direction = transform.right * hInput + transform.forward * vInput;
            _model._chController.Move(direction * _model.speed * Time.deltaTime);
        }
    }
}
