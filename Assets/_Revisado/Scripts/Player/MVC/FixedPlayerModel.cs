using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FixedPlayerModel : Entity
{
    [Header("References")]
    public Flashlight _flashlight;
    private Rigidbody _rb;
    private FixedPlayerController _controller;
    private FixedPlayerView _view;
    private Ray _movRay;
    private Vector3 _movDir = new Vector3();

    [Header("MouseMovement")]
    [SerializeField] float rotationSpeedY = 50f;
    [SerializeField] float rotationSpeedZ = 50f;

    [Header("Values")]
    [SerializeField] float _movSpeed = 5f;
    [SerializeField] float _rotSpeed = 2.5f;
    bool _isRunning = false;

    [Header("Nictofobia")]
    [SerializeField] bool _scared;
    [SerializeField] float _scaredTime;

    [Header("Inventory")]
    public Inventory _inventory = new Inventory();

    [Header("Camera")]
    [SerializeField] private Transform _camLookAtTransform;

    [Header("Physics")]
    [SerializeField] private float _movRayRange = .75f;
    [SerializeField] private LayerMask _movRayMask;

    [Header("PowerUps")]
    public PowerUp _staminaPU;
    public PowerUp _visionPU;
    public bool _pUStaminaActive;
    public bool _pUVisionActive;

    public bool Scared { get { return _scared; } set { _scared = value; } }

    private void Awake()
    {
        currentHP = _maxHP;
        currentStamina = _maxStamina;
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.angularDrag = 1f;

        _controller = GetComponent<FixedPlayerController>();
        _view = GetComponent<FixedPlayerView>();
        Invoke(nameof(AssignReferencesToManager), .1f);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //FixedCamController.Instance.Target = _camLookAtTransform;
    }

    private void Update()
    {
        _controller.UpdateKeys();
        _controller.MouseMovement(_flashlight.transform, rotationSpeedY, rotationSpeedZ);
    }

    private void FixedUpdate() => _controller.ListenFixedKeys();

    #region Movement Methods

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

    public void Run(bool value)
    {
        _isRunning = value;

        if (_isRunning && currentStamina > 0)
        {
            _movSpeed = 10f;
            if (!_pUStaminaActive)
            {
                currentStamina -= (MaxStamina / 10) * Time.deltaTime;
                GameManager.Instance.playerUI.UpdateUIStatus();
            }
        }
        else _movSpeed = 5f;

        if (!_isRunning) StartCoroutine(RefillStamina());
    }

    IEnumerator RefillStamina()
    {
        yield return new WaitForSeconds(2f);
        while (currentStamina < MaxStamina)
        {
            if (_isRunning) yield break;
            currentStamina += 15 * Time.deltaTime;
            GameManager.Instance.playerUI.UpdateUIStatus();
            yield return null;
        }
        currentStamina = MaxStamina;
    }

    #endregion

    #region Flashlight Methods

    public void FlashlightClick()
    {
        _flashlight.TurnOnOff();
        //_flashlight.
        UpdateNictoStatus();
    }

    public void FlashlightFocusedLight(bool activate)
    {
        _flashlight.TurnOnOffFocusedLight(activate);
    }
    #endregion


    #region Interaction Methods

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _viewRadius, _interactLayer);

        if (colliders.Length == 0) return;

        Collider closest = null;

        if (colliders.Length == 1 && InFieldOfView(colliders[0].transform.position)) closest = colliders[0];
        else
        {
            foreach (Collider collider in colliders)
            {
                if (InFieldOfView(collider.transform.position))
                {
                    if (closest == null) closest = collider;
                    else if (Vector3.Distance(collider.transform.position, _camLookAtTransform.position) < Vector3.Distance(closest.transform.position, _camLookAtTransform.position))
                    {
                        closest = collider;
                    }
                }
            }

            if (closest == null) return;
        }
        if (closest.TryGetComponent<Pickable_Object>(out Pickable_Object _item)) _item.PickUp();
        if (closest.TryGetComponent<Button>(out Button _button)) _button.Interact();
    }

    public void UseItem(Consumables item)
    {
        switch (item)
        {
            case Consumables.battery:
                _flashlight.ChangeBattery();
                break;
            case Consumables.healthPill:
                Heal();
                break;
            case Consumables.visionPill:
                _visionPU.UsePowerUp();
                break;
            case Consumables.staminaPill:
                _staminaPU.UsePowerUp();
                break;
        }
        GameManager.Instance.playerUI.UpdateUIStatus();
    }

    #endregion

    void Heal()
    {
        if (_inventory.healthPill > 0 & currentHP < MaxHP)
        {
            _inventory.healthPill--;
            currentHP = Mathf.Clamp(currentHP + 40f, currentHP, MaxHP);
        }
    }

    public override void TakeDamage(float dmgValue)
    {
        base.TakeDamage(dmgValue);
        GameManager.Instance.playerUI.UpdateUIStatus();
    }

    public void UpdateNictoStatus()
    {
        _scared = !_flashlight.IsLigthOn;
        if (_flashlight.IsLigthOn) return;
    }

    void AssignReferencesToManager()
    {
        GameManager.Instance.playerModel = this;
    }

    #region OnDrawGizmos

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Gizmos.color = Color.green;

        Vector3 dirA = GetDirFromAngle(_viewAngle / 2);
        Vector3 dirB = GetDirFromAngle(-_viewAngle / 2);

        Gizmos.DrawLine(transform.position, transform.position + dirA.normalized * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + dirB.normalized * _viewRadius);
    }

    Vector3 GetDirFromAngle(float angleInDegrees)
    {
        float angle = angleInDegrees + transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    #endregion
}
