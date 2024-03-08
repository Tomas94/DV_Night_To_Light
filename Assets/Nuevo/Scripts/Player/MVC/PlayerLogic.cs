using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

public class PlayerLogic : Entity
{
    [Header("Referencias")]
    PlayerController _controller;
    public CharacterController _chController;
    UI_InGame _interface;

    [Header("Inventario")]
    [SerializeField] public Transform _interactionPoint;
    public Inventory _inventory = new Inventory();

    [Header("Linterna y Nictofobia")]
    [SerializeField] Flashlight _flashlight;
    [SerializeField] bool _nictoActive;
    [SerializeField] bool _scared;
    [SerializeField] float _currentScaredTimer;
    [SerializeField] float _scareResistanceTime;

    [Header("Movimiento")]
    [SerializeField] Transform _groundCheck;
    Vector3 _fallingSpeedVector = Vector3.zero;

    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] bool _isRunning;

    [Header("PowerUps")]
    public PowerUp _staminaPU;
    public PowerUp _visionPU;
    public bool _pUStaminaActive;
    public bool _pUVisionActive;

    public bool Scared { get { return _scared; } set { _scared = value; } }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _chController = GetComponent<CharacterController>();
        _controller = GetComponent<PlayerController>();

        currentHP = _maxHP;
        currentStamina = _maxStamina;
        speed = _walkSpeed;

        GameManager.Instance.SetPlayerRef(this);
    }

    private void Start()
    {
        _flashlight = GameManager.Instance.Flashlight;
        _interface = GameManager.Instance.UI;

        if (_flashlight.IsLigthOn) _scared = false; else _scared = true;
    }

    private void Update()
    {
        _controller.UpdateKeys();
        Fall();
        IsScared();
    }

    #region Movimiento (Correr, Stamina Refill y Caida )

    public void Run(bool running)
    {
        _isRunning = running;
        if (_isRunning && currentStamina > 0)
        {
            speed = _runSpeed;
            if (!_pUStaminaActive)
            {
                currentStamina -= (MaxStamina / 10) * Time.deltaTime;
                _interface.UpdateUIStatus();
            }
        }
        else speed = _walkSpeed;

        if (!_isRunning) StartCoroutine(RefillStamina());
    }

    public void Fall()
    {
        float fallingSpeed = -8f;
        float gravity = -9.81f;
        float groundDistance = 0.4f;
        bool isGrounded;

        _fallingSpeedVector.y += gravity * Time.deltaTime;

        isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, LayerMask.NameToLayer("Ground"));

        if (isGrounded && _fallingSpeedVector.y < 0) _fallingSpeedVector.y = fallingSpeed;

        _chController.Move(_fallingSpeedVector * Time.deltaTime);
    }

    public IEnumerator RefillStamina()
    {
        yield return new WaitForSeconds(2f);
        while (currentStamina < MaxStamina)
        {
            if (_isRunning) yield break;
            currentStamina += 15 * Time.deltaTime;
            _interface.UpdateUIStatus();
            yield return null;
        }
        currentStamina = MaxStamina;
    }
    #endregion

    #region Linterna (Encendido, Laser y Nictofobia)

    public void FlashlightClick()
    {
        _flashlight.TurnOnOff();
        UpdateNictoStatus();
    }

    public void FlashlightFocusedLight(bool activate)
    {
        _flashlight.TurnOnOffFocusedLight(activate);
    }

    void IsScared()
    {
        if (_scared && !_nictoActive)
        {
            _currentScaredTimer += Time.deltaTime;
            if (_currentScaredTimer >= _scareResistanceTime)
            {
                StartCoroutine(AfraidDamage());
            }
            Debug.Log("A Oscuras");
        }
        else if (_currentScaredTimer == 0f) return;
        else
        {
            _currentScaredTimer = 0f;
            Debug.Log("En la Luz");
        }
    }

    IEnumerator AfraidDamage()
    {
        _nictoActive = true;
        while (_scared)
        {
            yield return new WaitForSeconds(1);
            TakeDamage(5);
            yield return null;
        }
        _nictoActive = false;
    }


    public void UpdateNictoStatus()
    {
        _scared = !_flashlight.IsLigthOn;
        if (_flashlight.IsLigthOn) return;
    }

    public void SwitchLight()
    {
        _flashlight.UvLightOnOff();
    }
    #endregion

    #region Interaccion y Uso de Objetos

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
                Debug.Log(collider.name);
                if (InFieldOfView(collider.transform.position))
                {
                    if (closest == null) closest = collider;
                    else if (Vector3.Distance(collider.transform.position, _interactionPoint.position) < Vector3.Distance(closest.transform.position, _interactionPoint.position))
                    {
                        closest = collider;
                    }
                }
            }
            Debug.Log(closest);
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
        _interface.UpdateUIStatus();
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
        _interface.UpdateUIStatus();
    }

    public override void Die()
    {
        GameManager.Instance.Checkpoint.LoadStatus(this);
        Restore();
    }

    void Restore()
    {
        currentHP = MaxHP;
        currentStamina = MaxStamina;
        _flashlight.RestoreFullCharge();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LuzTrigger")
        {
            _scared = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LuzTrigger" && !_flashlight.IsLigthOn) _scared = true;
    }

    #region OnDrawGizmos

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_interactionPoint.position, _viewRadius);

        Gizmos.color = Color.green;

        Vector3 dirA = GetDirFromAngle(_viewAngle / 2);
        Vector3 dirB = GetDirFromAngle(-_viewAngle / 2);

        Gizmos.DrawLine(_interactionPoint.position, _interactionPoint.position + dirA.normalized * _viewRadius);
        Gizmos.DrawLine(_interactionPoint.position, _interactionPoint.position + dirB.normalized * _viewRadius);
    }

    Vector3 GetDirFromAngle(float angleInDegrees)
    {
        float angle = angleInDegrees + transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }


    #endregion
}
