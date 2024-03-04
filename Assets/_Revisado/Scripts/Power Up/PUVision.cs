using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUVision : PowerUp
{
    void Start()
    {
        _duration = 15;
    }
    
    public override void UsePowerUp()
    {
        if (GameManager.Instance.playerModel._pUVisionActive == true) return;
        if (GameManager.Instance.playerModel._inventory.visionPill > 0)
        {
            GameManager.Instance.playerModel._inventory.visionPill--;
            base.UsePowerUp();
        }
    }

    public override void ActivatePowerUp()
    {
        GameManager.Instance.playerModel._pUVisionActive = true;
    }

    public override void DeactivatePowerUp()
    {
        GameManager.Instance.playerModel._pUVisionActive = false;
    }


}
