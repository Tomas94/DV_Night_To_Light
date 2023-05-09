using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    [Header("Movement")]
    public float speed;
    public float runSpeed;
    public float crouchSpeed;
    public bool isCrouch;
    public int crouchHeigth;
    public int standHeight;
    public float gravity = -9.81f;

    [Header("Hp")]
    public int maxHP;
    public int currentHP;

    [Header("Inventory")]
    public int batteries;
    public int bandages;

    public PlayerStats(float speedx, float runSpeedx, float crouchSpeedx, bool isCrouchx, int crouchHeightx, int standHeightx) 
    {
        speed = speedx;
        runSpeed = runSpeedx;
        crouchSpeed = crouchSpeedx;
        crouchHeigth = crouchHeightx;
        standHeight = standHeightx;
        isCrouch = isCrouchx;
    }

    public PlayerStats(int maxHPx, int batteriesx, int bandagesx/*, bool isNictox*/)
    {
        maxHP = maxHPx;
        currentHP = maxHP;
        batteries = batteriesx;
        bandages = bandagesx;
    }
}
