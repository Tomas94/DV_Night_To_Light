using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Entity : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float _maxHP;
    public float currentHP;
    [SerializeField] protected float _maxStamina;
    public float currentStamina;
    public float speed;

    [Header("Vision Values")]
    [SerializeField] protected float _viewRadius;
    [SerializeField] protected float _viewAngle;
    [SerializeField] protected LayerMask _interactLayer;

    public float MaxHP { get { return _maxHP; } }
    public float MaxStamina { get { return _maxStamina; } }

    public virtual void TakeDamage(float dmgValue)
    {
        currentHP -= dmgValue;
        
        if (currentHP <= 0)
        {
            //Death()
            Debug.Log("Moriste, pete");
            Die();
        }
    }

    public abstract void Die();

    public bool InFieldOfView(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        if (!InLineOfSight(dir)) return false;
        if (dir.magnitude > _viewRadius) return false;
        return Vector3.Angle(transform.forward, dir) <= _viewAngle / 2;
    }

    public bool InLineOfSight(Vector3 dir)
    {
        return Physics.Raycast(transform.position, dir, dir.magnitude, _interactLayer);
    }
}
