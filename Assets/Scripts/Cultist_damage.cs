using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist_damage : MonoBehaviour
{
    public PlayerController player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.currentHP--;
        }
    }
}
