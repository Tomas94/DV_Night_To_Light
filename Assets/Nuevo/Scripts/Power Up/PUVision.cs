public class PUVision : PowerUp
{
    public override void UsePowerUp()
    {
        if (_player._pUVisionActive == true) return;
        if (_player._inventory.visionPill > 0)
        {
            _player._inventory.visionPill--;
            base.UsePowerUp();
        }
    }

    public override void ActivatePowerUp()
    {
        _player._pUVisionActive = true;
    }

    public override void DeactivatePowerUp()
    {
        _player._pUVisionActive = false;
    }
}
