using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FixedPlayerModel : Entity
{

    [Header("Values")]
    [SerializeField] private float _movSpeed = 5f;
    [SerializeField] private float _rotSpeed = 2.5f;

    [Header("Nictofobia")]
    [SerializeField] bool _scared;
    [SerializeField] float _scaredTime;

    [Header("Inventario")]
    [SerializeField] Inventory _inventario = new Inventory();

    [Header("Camera")]
    [SerializeField] private Transform _camLookAtTransform;

    [Header("Physics")]
    [SerializeField] private float _movRayRange = .75f;
    [SerializeField] private LayerMask _movRayMask;

    private Rigidbody _rb;
    private FixedPlayerController _controller;
    private FixedPlayerView _view;

    private Ray _movRay;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.angularDrag = 1f;

        _controller = GetComponent<FixedPlayerController>();
        _view = GetComponent<FixedPlayerView>();
    }

    private void Start()
    {
        FixedCamController.Instance.Target = _camLookAtTransform;
    }

    private void Update()
    {
        _controller.UpdateKeys();
    }

    private void FixedUpdate()
    {
        _controller.ListenFixedKeys();
    }

    private Vector3 _movDir = new Vector3();

    public void Movement(Vector3 dir)
    {
        if (dir.sqrMagnitude != 0)
        {
            if (dir.z != 0)
            {
                _movDir = (transform.forward * dir.z);

                if (CanMove(_movDir)) _rb.MovePosition(transform.position + _movDir * _movSpeed * Time.fixedDeltaTime);
            }

            if (dir.x != 0)
            {
                transform.Rotate(0f, dir.x * _rotSpeed, 0f);
            }
        }

        _view.SetMovement(dir.z);
    }

    private bool CanMove(Vector3 dir)
    {
        _movRay = new Ray(transform.position, dir);

        return !Physics.Raycast(_movRay, _movRayRange, _movRayMask);
    }

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _viewRadius, _interactLayer);

        if (colliders.Length == 0) return;

        Collider closest = null;

        foreach (Collider collider in colliders)
        {
            if(InFieldOfView(collider.transform.position))
            {
               if(closest == null) closest = collider;
               else if(Vector3.Distance(collider.transform.position , _camLookAtTransform.position) < Vector3.Distance(closest.transform.position, _camLookAtTransform.position))
                {
                    closest = collider;
                }
            }
        }

        if (closest == null) return;

        if (closest.TryGetComponent<Pickable>(out Pickable _item)) _item.Pickup();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_camLookAtTransform.position, _viewRadius);
        
        Gizmos.color = Color.green;

        Vector3 dirA = GetDirFromAngle(_viewAngle / 2);
        Vector3 dirB = GetDirFromAngle(-_viewAngle / 2);

        Gizmos.DrawLine(_camLookAtTransform.position, _camLookAtTransform.position + dirA.normalized * _viewRadius);
        Gizmos.DrawLine(_camLookAtTransform.position, _camLookAtTransform.position + dirB.normalized * _viewRadius);
    }

    Vector3 GetDirFromAngle(float angleInDegrees)
    {
        float angle = angleInDegrees + transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
