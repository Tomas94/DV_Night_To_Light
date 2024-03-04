using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlayerController : MonoBehaviour
{
    private FixedPlayerModel _model;
    Vector3 _dir = new();

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
        if (Input.GetKeyDown(KeyCode.E)) _model.Interact();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //model.Cambiar entre UV y normal
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _model.FlashlightClick();
            //model.Prender o Apagar Linterna
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _model.UseItem(Consumables.battery);
            //model.Prender o Apagar Linterna
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _model.UseItem(Consumables.healthPill);
            //model. Usar pastilla vida
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _model.UseItem(Consumables.staminaPill);
            //model. Usar pastilla Stamina
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //model. Usar pastilla vision.
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _model.Run(true);
            //model. Correr
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _model.Run(false);
            //model. Dejar de Correr
        }

        if (Input.GetMouseButton(1))
        {
            _model.FlashlightFocusedLight(true);

            if (Input.GetMouseButtonDown(0))
            {
                //model. Destello.
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            _model.FlashlightFocusedLight(false);
        }
    }

    public void MouseMovement(Transform _mouseObject, float rotSpdY, float rotSpdZ)
    {
        float mouseY = Input.GetAxis("Mouse X") * rotSpdY * Time.deltaTime;
        float mouseZ = Input.GetAxis("Mouse Y") * rotSpdZ * Time.deltaTime;

        // Rotar el objeto en el eje Y según el movimiento del mouse horizontal
        _mouseObject.Rotate(_mouseObject.up, mouseY);

        // Rotar el objeto en el eje Z según el movimiento del mouse vertical
        _mouseObject.Rotate(_mouseObject.forward, mouseZ);

        _mouseObject.rotation = new Quaternion(0, _mouseObject.rotation.y, _mouseObject.rotation.z, _mouseObject.rotation.w);
    }
}
