using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] FixedPlayerModel _playerModel;
    [SerializeField] Flashlight _flashLight;

    [Header("Fills")]
    [SerializeField] Image _lifeBar;
    [SerializeField] Image _staminaBar;
    [SerializeField] Image _lightBar;

    [Header("Icono Linterna")]
    public Image _flashlightIcon;

    [Header("Sprites Tipos de luz")]
    [SerializeField] Sprite _light;
    [SerializeField] Sprite _ultravioletLight;

    [Header("Texto Contadores")]
    [SerializeField] TextMeshProUGUI _batteriesAmount;
    [SerializeField] TextMeshProUGUI _healthPillAmount;
    [SerializeField] TextMeshProUGUI _staminaPillAmount;
    [SerializeField] TextMeshProUGUI _visionPillAmoun;

    [Header("PowerUp Icons")]
    public Image _staminaPUFill;
    [SerializeField] Image _visionPUFill;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchLightMode();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdatePlayerStatus();
            UpdateItemAmountStatus();
        }
    }

    void SwitchLightMode()
    {
        _lightBar.sprite = _lightBar.sprite == _light ? _ultravioletLight : _light;
    }

    public void UpdateUIStatus()
    {
        UpdateItemAmountStatus();
        UpdatePlayerStatus();
    }

    void UpdatePlayerStatus()
    {
        _lifeBar.fillAmount = CalculatePercentage(_playerModel.currentHP, _playerModel.MaxHP);
        _staminaBar.fillAmount = CalculatePercentage(_playerModel.currentStamina, _playerModel.MaxStamina);
        _lightBar.fillAmount = CalculatePercentage(_flashLight.CurrentChargeAmount, _flashLight.MaxChargeAmount);
    }

    void UpdateItemAmountStatus()
    {
        _healthPillAmount.text = "x" + _playerModel._inventory.healthPill;
        _staminaPillAmount.text = "x" + _playerModel._inventory.staminaPill;
        _visionPillAmoun.text = "x" + _playerModel._inventory.visionPill;
        _batteriesAmount.text = "x" + _playerModel._inventory.battery;
    }


    float CalculatePercentage(float _currentValue, float _maxValue) => Mathf.Clamp01((100 / _maxValue) * (_currentValue / 100));   

    public IEnumerator UpdatePowerUPIconStatus(float maxValue, float duration, Image icon)
    {
        Debug.Log("En la corrutina de icono");
        while (icon.fillAmount > 0.05f)
        {
            Debug.Log("En el while de corrutina de icono");
            icon.fillAmount -= (maxValue / duration) * Time.deltaTime;
            yield return null;
        }
        icon.fillAmount = 0;
    }
}
