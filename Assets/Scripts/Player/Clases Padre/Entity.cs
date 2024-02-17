using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //protected float _movSpeed;

    [Header("Stats")]
    public float currentHP;
    public float currentStamina;
    protected float _maxHP;
    protected float _maxStamina;

    [Header("Vision Values")]
    [SerializeField] protected float _viewRadius;
    [SerializeField] protected float _viewAngle;
    [SerializeField] protected LayerMask _interactLayer;

    public virtual void TakeDamage()
    {
        currentHP--;
    }

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
