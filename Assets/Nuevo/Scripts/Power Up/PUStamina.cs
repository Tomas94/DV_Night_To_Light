public class PUStamina : PowerUp
{
    public override void UsePowerUp()
    {
        if (_player._pUStaminaActive == true) return;
        if (_player._inventory.staminaPill > 0)
        {
            _player._inventory.staminaPill--;

            base.UsePowerUp();
        }
    }
   
    void InfiniteStamina()
    {
        _player.currentStamina = _player.MaxStamina;
        _uI._staminaPUFill.fillAmount = 1;
    }
    
    public override void ActivatePowerUp()
    {
        _player._pUStaminaActive = true;
        InfiniteStamina();
        StartCoroutine(_uI.UpdatePowerUPIconStatus(1, _duration, _uI._staminaPUFill));
    }

    public override void DeactivatePowerUp()
    {
        _player._pUStaminaActive = false;
    }
    
}
