using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nictofobia : MonoBehaviour
{
    public bool nictofobia;
    public float startTime = 0f;
    float endTime = 5f;
    public PlayerController player;

    private void Start()
    {
        nictofobia = true;
    }

    private void Update()
    {
        if (nictofobia)
        {
            startTime += Time.deltaTime;
            if (startTime >= endTime)
            {
                startTime = 0f;
                player.TakeDamage();             
            }
        }
        else
        {
            startTime = 0.0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "LuzTrigger")
        {
            nictofobia = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nictofobia = true;
    }

     public void IsFeared()
     {
         if (nictofobia)
         {
             startTime += Time.deltaTime;
             if (startTime >= endTime)
             {
                 startTime = 0f;
                 player.TakeDamage();             
             }
         }
         else
         {
             startTime = 0.0f;
         }
     }
}
