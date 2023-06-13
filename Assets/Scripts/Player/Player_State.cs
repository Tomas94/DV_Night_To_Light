using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : Entity
{
    [Header("Referencia UI")]
    [SerializeField] UI_Player uIPlayer;

    [Header("Stats")]
    public int currentHP;
    public int cantidadBaterias;
    public int cantidadVendajes;

    [Header("Timer Nictofobia")]
    public bool isNicto;
    float startTime = 0f;
    float endTime = 5f;

    [Header("Linterna")]
    [SerializeField] Linterna linterna;

    private void Start()
    {
        maxHP = 3;
        currentHP = maxHP;
        cantidadBaterias = 0;
        cantidadVendajes = 0;
    }

    private void Update()
    {
        ObjectUse();
        IsScared(isNicto);
    }

    public void ObjectUse()
    {
        Heal();
        ChangeBattery();
    }

    void Heal()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentHP < maxHP && cantidadVendajes > 0)
        {
            currentHP++;
            cantidadVendajes--;
        }
    }

    void ChangeBattery()
    {
        if (Input.GetKeyDown(KeyCode.R) && /*currentCharge < maxCharge &&*/ cantidadBaterias > 0)
        {
            linterna.currentCharge = linterna.maxCharge;
            cantidadBaterias--;
            uIPlayer.BatteriesOnHold(cantidadBaterias);
        }
    }

    void IsScared(bool nicto)
    {
        if (nicto)
        {
            startTime += Time.deltaTime;
            if (startTime >= endTime)
            {
                startTime = 0f;
                TakeDamage();
            }
            Debug.Log("A Oscuras");
        }
        else
        {
            startTime = 0.0f;
            Debug.Log("En la Luz");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LuzTrigger")
        {
            isNicto = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "LuzTrigger")
        {
            isNicto = true;
        }
    }
}
