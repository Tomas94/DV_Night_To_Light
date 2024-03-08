using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aura2API;
using System;

public class PUVision : PowerUp
{
    [SerializeField] AuraCamera _camera;

    private void Start()
    {
        _camera = GetComponentInChildren<AuraCamera>();
    }

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
