using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTriggerZone : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int _index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FixedPlayerController>())
        {
            FixedCamController.Instance.SetCamera(_index);
        }
    }
}
