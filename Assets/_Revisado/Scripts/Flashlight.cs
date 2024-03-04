using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Variables Linterna")]
    Light _pointLight;                          //Referencia Luz
    [SerializeField] float _maxChargeAmount;
    [SerializeField] float _currentChargeAmount;
    [SerializeField] float _chargeDuration;
    [SerializeField] bool _isLigthOn;
    //variable de ataque laser

    public bool focuseActive = false;

    [Header("Variables Laser")]
    [SerializeField] LayerMask _reflectiveMask;
    [SerializeField] LineRenderer _laser;                        //Referencia Laser
    Ray _ray;
    //RaycastHit _hit;
    [SerializeField] Light _laserHitPoint;
    float _laserLenght = 30;
    float _reflectionsCount;


    public bool IsLigthOn { get { return _isLigthOn; } }
    public float MaxChargeAmount { get { return _maxChargeAmount; } }
    public float CurrentChargeAmount { get { return _currentChargeAmount; } }

    private void Awake()
    {
        _pointLight = GetComponentInChildren<Light>();
        _laser = GetComponentInChildren<LineRenderer>();
        _laserHitPoint = transform.GetChild(1).GetComponentInChildren<Light>();
        UpdateFlashlightState();
        Invoke(nameof(SetReferences), 0.2f);
        _currentChargeAmount = MaxChargeAmount;
    }

    private void Update()
    {
        if (IsLigthOn)
        {
            if (CurrentChargeAmount <= 0)
            {
                _isLigthOn = false;
                _pointLight.enabled = false;
                _currentChargeAmount = 0;
                GameManager.Instance.playerUI._flashlightIcon.enabled = IsLigthOn;
                return;
            }
            _currentChargeAmount -= (MaxChargeAmount / _chargeDuration) * Time.deltaTime;
            GameManager.Instance.playerUI.UpdateUIStatus();

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
        GameManager.Instance.playerUI._flashlightIcon.enabled = _isLigthOn; 
            }

    public void TurnOnOffFocusedLight(bool isOn)
    {
        if (!_isLigthOn) return;

        _pointLight.enabled = !isOn;
        _laser.enabled = isOn;
        ActivateLaser();

    }

    public void ChangeBattery()
    {
        Inventory playerInv = GameManager.Instance.playerModel._inventory;
        if (CurrentChargeAmount >= MaxChargeAmount || playerInv.battery <= 0) return;
        playerInv.battery--;
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
                count++;
                _laser.SetPosition(_laser.positionCount - 1, lastHit.point);
                _laserHitPoint.enabled = true;
                _laserHitPoint.transform.position = lastHit.point;
                remainLenght = 0;
            }
            else
            {
                _laser.SetPosition(_laser.positionCount - 1, _ray.GetPoint(remainLenght));
                remainLenght = 0;
            }
        }

        focuseActive = true;
        //Debug.Log(count);
        /*for (int i = 0; i < _reflectionsCount; i++)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainLenght, _laserMask))
            {
                Debug.Log(_hit.transform.name);
                if (_hit.transform.tag == "Espejo")
                {
                    _laser.positionCount += 1;
                    _laser.SetPosition(_laser.positionCount - 1, _hit.point);
                    remainLenght -= Vector3.Distance(_ray.origin, _hit.point);
                    _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));
                }
                else if (_hit.transform.tag == "Enemy")
                {
                    //if (Input.GetMouseButtonDown(0)) StartCoroutine(LaserAttack(_hit.transform.gameObject));
                    _laser.positionCount += 1;
                    _laser.SetPosition(_laser.positionCount - 1, _hit.point);
                    _laserHitPoint.enabled = true;
                    _laserHitPoint.transform.position = _hit.point;
                }
                else if (_hit.transform.tag == "Boton")
                {
                    //     hit.transform.GetComponent<Puzzles>().puzzleCompleto = true;
                }
                else
                {
                    _laser.positionCount += 1;
                    _laser.SetPosition(_laser.positionCount - 1, _hit.point);
                    _laserHitPoint.enabled = true;
                    _laserHitPoint.transform.position = _hit.point;
                }

            }
            
        }*/
    }

    void SetReferences()
    {
        GameManager.Instance.playerModel._flashlight = this;
        GameManager.Instance.playerModel.UpdateNictoStatus();
    }
}
