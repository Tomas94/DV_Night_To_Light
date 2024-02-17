using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlayerView : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator _anim;
    [SerializeField] private string _zAxisName = "zAxis";

    private void Start()
    {
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
    }

    public void SetMovement(float zAxis)
    {
        _anim.SetFloat(_zAxisName, zAxis);
    }
}
