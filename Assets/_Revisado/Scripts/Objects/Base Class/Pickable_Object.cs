using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(SphereCollider))]

public abstract class Pickable_Object : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5)] float _detectionDiameterTrigger = 1.5f;

    [SerializeField] protected string _sFXName;

    private void Awake()
    {
        CreateDetectionTrigger();
    }

    public void CreateDetectionTrigger()
    {
        SphereCollider _sphereTrigger = gameObject.GetComponent<SphereCollider>();
        _sphereTrigger.isTrigger = true;
        _sphereTrigger.radius = _detectionDiameterTrigger / 2f;
    }

    public void PickUp()
    {
        AudioManager.Instance.PlaySFX(_sFXName);
        AddObjectToInventory();
        GameManager.Instance.playerUI.UpdateUIStatus();
    }

    protected virtual void AddObjectToInventory()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUp();
        }
    }

    void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (TryGetComponent<SphereCollider>(out var collider))
            {
                collider.radius = _detectionDiameterTrigger / 2f;                // Actualizar el diámetro del collider
            }
        }
    }
}
