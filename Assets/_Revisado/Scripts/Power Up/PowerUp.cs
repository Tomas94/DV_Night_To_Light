using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float _duration;

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