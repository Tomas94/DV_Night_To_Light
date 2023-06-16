using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int maxHP;
    public int currentHP;
    protected float speed;

    public virtual void TakeDamage()
    {       
            currentHP--;
            Debug.Log("Perdio Vida, hp actual: " + currentHP);     
    }

    public virtual void Movement()
    {
        Vector3 direction;
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        CharacterController chController = GetComponent<CharacterController>();

        if (chController != null)
        {
            direction = transform.right * hInput + transform.forward * vInput;
            chController.Move(direction * speed * Time.deltaTime);
        }
    }
}
