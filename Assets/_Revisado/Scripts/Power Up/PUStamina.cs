using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUStamina : PowerUp
{
    private void Start()
    {
        _duration = 10;
    }

    public override void UsePowerUp()
    {
        if (GameManager.Instance.playerModel._pUStaminaActive == true) return;
        if (GameManager.Instance.playerModel._inventory.staminaPill > 0)
        {
            GameManager.Instance.playerModel._inventory.staminaPill--;

            base.UsePowerUp();
        }
    }
   
    void InfiniteStamina()
    {
        GameManager.Instance.playerModel.currentStamina = GameManager.Instance.playerModel.MaxStamina;
        GameManager.Instance.playerUI._staminaPUFill.fillAmount = 1;
    }
    
    public override void ActivatePowerUp()
    {
        GameManager.Instance.playerModel._pUStaminaActive = true;
        InfiniteStamina();
        StartCoroutine(GameManager.Instance.playerUI.UpdatePowerUPIconStatus(1, _duration, GameManager.Instance.playerUI._staminaPUFill));
    }

    public override void DeactivatePowerUp()
    {
        GameManager.Instance.playerModel._pUStaminaActive = false;
    }

}
