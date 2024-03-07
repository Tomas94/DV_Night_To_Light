using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected PlayerLogic _player;
    protected UI_InGame _uI;

    [SerializeField] protected float _duration;

    private void Start()
    {
        if(_duration <= 0) _duration = 10;
        _player = GameManager.Instance.Player;
        _uI = GameManager.Instance.UI;
    }

    public abstract void ActivatePowerUp();
    public abstract void DeactivatePowerUp();

    public virtual void UsePowerUp()
    {
        StartCoroutine(nameof(PowerUpActivation));
    }

    public virtual IEnumerator PowerUpActivation()
    {
        ActivatePowerUp();
        yield return new WaitForSeconds(_duration);
        DeactivatePowerUp();
        yield return null;
    }
}