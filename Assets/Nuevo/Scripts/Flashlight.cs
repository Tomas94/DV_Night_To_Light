using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] UI_InGame _uInterface;
    PlayerLogic _player;

    [Header("Variables Linterna")]
    Light _pointLight;                          //Referencia Luz
    [SerializeField] float _maxChargeAmount;
    [SerializeField] float _currentChargeAmount;
    [SerializeField] float _chargeDuration;
    [SerializeField] bool _isLigthOn;
    //variable de ataque laser

    [Header("Tipos de Luz")]
    Color _normalLight = new Color(183 / 255f, 183 / 255f, 183 / 255f, 47 / 255f);
    Color _uvLight = new Color(72 / 255f, 0 / 255f, 135 / 255f, 47 / 255f);
    BoxCollider _uvTrigger;

    public bool focuseActive = false;

    [Header("Variables Laser")]
    [SerializeField] LayerMask _reflectiveMask;
    LineRenderer _laser;                        //Referencia Laser
    Ray _ray;
    [HideInInspector] public Light _laserHitPoint;
    [SerializeField] float _laserLenght = 30;

    [Header("Variables Ataque Laser")]
    [SerializeField] float _laserDmg;
    [SerializeField] float _cd;
    [SerializeField] bool _canLaserAttack;

    public bool IsLigthOn { get { return _isLigthOn; } }
    public float MaxChargeAmount { get { return _maxChargeAmount; } }
    public float CurrentChargeAmount { get { return _currentChargeAmount; } }

    private void Awake()
    {
        _pointLight = GetComponentInChildren<Light>();
        _laser = GetComponentInChildren<LineRenderer>();
        _laserHitPoint = transform.GetChild(1).GetComponentInChildren<Light>();
        _uvTrigger = _pointLight.GetComponent<BoxCollider>();

        _currentChargeAmount = MaxChargeAmount;
        _pointLight.color = _normalLight;
        _canLaserAttack = true;
        UpdateFlashlightState();
        
        GameManager.Instance.SetFlashlightRef(this);
        
        //opcional
        _isLigthOn = _pointLight.enabled;

    }

    private void Start()
    {
        _uInterface = GameManager.Instance.UI;
        _uInterface._flashlightIcon.enabled = _isLigthOn;
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (IsLigthOn)
        {
            if (CurrentChargeAmount <= 0)
            {
                _isLigthOn = false;
                _pointLight.enabled = false;
                _laserHitPoint.enabled = false;
                _laser.enabled = false;
                _currentChargeAmount = 0;

                _uInterface._flashlightIcon.enabled = IsLigthOn;
                return;
            }
            _currentChargeAmount -= (MaxChargeAmount / _chargeDuration) * Time.deltaTime;
            _uInterface.UpdateUIStatus();

        }
    }

    void UpdateFlashlightState()
    {
        _isLigthOn = _pointLight.enabled;
    }

    public void TurnOnOff()
    {
        if (CurrentChargeAmount <= 0) return;
        if (_laser.enabled) return;

        _isLigthOn = !_isLigthOn;
        _pointLight.enabled = _isLigthOn;
        _uInterface._flashlightIcon.enabled = _isLigthOn;
        AudioManager.Instance.PlaySFX("ClickLinterna");
    }

    public void TurnOnOffFocusedLight(bool isOn)
    {
        if (!_isLigthOn) return;

        _pointLight.enabled = !isOn;
        _laser.enabled = isOn;
        ActivateLaser();
        if (!isOn) _laserHitPoint.enabled = false;

    }

    public void ChangeBattery()
    {
        if (CurrentChargeAmount >= MaxChargeAmount || _player._inventory.battery <= 0) return;
        AudioManager.Instance.PlaySFX("RecargarBatería");
        _player._inventory.battery--;
        _currentChargeAmount = Mathf.Clamp(_currentChargeAmount + (_maxChargeAmount * .5f), 0, _maxChargeAmount);
    }

    void ActivateLaser()
    {
        float remainLenght = _laserLenght;
        float count = 0;
        _ray = new Ray(_laser.transform.position, _laser.transform.forward);

        _laser.positionCount = 1;
        _laser.SetPosition(0, _laser.transform.position);

        while (remainLenght > 0)
        {
            _laser.positionCount++;
            RaycastHit lastHit;

            if (Physics.Raycast(_ray.origin, _ray.direction, out lastHit, remainLenght, _reflectiveMask))
            {
                count++;
                _laser.SetPosition(_laser.positionCount - 1, lastHit.point);
                remainLenght = _laserLenght * 2.5f;
                _ray = new Ray(lastHit.point, Vector3.Reflect(_ray.direction, lastHit.normal));

                if (lastHit.transform.TryGetComponent<Button>(out Button reflectiveSwitch))
                {
                    reflectiveSwitch.Interact();
                    Debug.Log("toco el objeto: " + lastHit.transform.name);
                }
            }
            else if (Physics.Raycast(_ray.origin, _ray.direction, out lastHit, remainLenght))
            {
                if (Input.GetMouseButtonDown(0) && _canLaserAttack && lastHit.transform.CompareTag("Enemy"))
                {
                    Entity enemy = lastHit.transform.GetComponentInChildren<Entity>();
                    if (enemy != null)
                    {
                        if (_currentChargeAmount > _maxChargeAmount * 0.1f) StartCoroutine(LaserAttack(enemy));
                    }
                }
                count++;
                _laser.SetPosition(_laser.positionCount - 1, lastHit.point);
                _laserHitPoint.enabled = true;
                _laserHitPoint.transform.position = lastHit.point;
                remainLenght = 0;
            }
            else
            {
                _laser.SetPosition(_laser.positionCount - 1, _ray.GetPoint(remainLenght));
                _laserHitPoint.enabled = false;
                remainLenght = 0;
            }
        }

        focuseActive = true;
    }

    IEnumerator LaserAttack(Entity enemy)
    {
        _currentChargeAmount -= _maxChargeAmount * .1f;
        _canLaserAttack = false;
        enemy.TakeDamage(_laserDmg);
        yield return new WaitForSeconds(_cd);
        _canLaserAttack = true;
    }

    public void UvLightOnOff()
    {
        if (_pointLight.color == _normalLight)
        {
            _pointLight.color = _uvLight;
            _uvTrigger.enabled = true;
        }
        else
        {
            _uvTrigger.enabled = false;
            _pointLight.color = _normalLight;
        }

        //_pointLight.color = _pointLight.color == _normalLight ? _uvLight : _normalLight;

        _uInterface.SwitchLightMode();
    }

    public bool isUVActive()
    {
        return _pointLight.color == _uvLight;
    }

    public void RestoreFullCharge()
    {
        _currentChargeAmount = _maxChargeAmount;
    }
}
