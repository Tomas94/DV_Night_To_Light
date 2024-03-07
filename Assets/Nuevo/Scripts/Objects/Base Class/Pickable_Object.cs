using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(SphereCollider))]

public abstract class Pickable_Object : MonoBehaviour
{
    Outline _outline;

    public PlayerLogic _player;
    [SerializeField] UI_InGame _uInteface;
    [SerializeField][Range(0.1f, 5)] float _detectionRadiusTrigger = 1f;
    [SerializeField] protected string _sFXName;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        CreateDetectionTrigger();
    }

    public void CreateDetectionTrigger()
    {
        SphereCollider _sphereTrigger = gameObject.GetComponent<SphereCollider>();
        _sphereTrigger.isTrigger = true;
        _sphereTrigger.radius = _detectionRadiusTrigger;
    }

    public void PickUp()
    {
        AudioManager.Instance.PlaySFX(_sFXName);
        AddObjectToInventory();
        _uInteface.UpdateUIStatus();
    }

    protected virtual void AddObjectToInventory()
    {
        Destroy(gameObject);
    }


    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent<PlayerLogic>(out PlayerLogic player)) return;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
           _outline.enabled = true;
        }
        else
        {
            _outline.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _outline.enabled = false;
    }

    void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (TryGetComponent<SphereCollider>(out var collider))
            {
                collider.radius = _detectionRadiusTrigger / 2f;                // Actualizar el diámetro del collider
            }
        }
    }
}
