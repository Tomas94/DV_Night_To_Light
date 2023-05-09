using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("Timer Nictofobia")]
    float startTime = 0f;
    float endTime = 5f;

    public bool isNicto;

    public UI_Player _uI;
    FlashlightScript1 flashLight;
    public PlayerStats status = new PlayerStats(3, 0, 0);

    private void Awake()
    {
        _uI = GameObject.Find("UI_Player").GetComponent<UI_Player>();
        flashLight = GameObject.Find("Flashlight").GetComponent<FlashlightScript1>();
    }

    void Update()
    {   
        CurrentHealth();
        IsFeared();
        ObjectUse();
        _uI.LifeBarState(status.currentHP);

        if (Input.GetKeyDown(KeyCode.Z)){
            status.bandages++;
            status.batteries++;
            _uI.BandagesOnHold(status.bandages);
        }
        if (Input.GetKeyDown(KeyCode.X))status.currentHP--;
        
    }
    
    public void TakeDamage()
    {
        status.currentHP--;
        Debug.Log("Vida Actual: " + status.currentHP);
    }

    void CurrentHealth()
    {
        switch (status.currentHP)
        {
            case int n when n == status.maxHP:
                break;

            case 2:
                break;

            case 1:
                break;

            case 0:
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Moriste");
                break;
        }
    }

    public void IsFeared()
    {
        if (isNicto)
        {
            startTime += Time.deltaTime;
            if (startTime >= endTime)
            {
                startTime = 0f;
                TakeDamage();
            }
        }
        else
        {
            startTime = 0.0f;
        }
    }

    void ObjectUse()
    {
        if (Input.GetKeyDown(KeyCode.Q) && status.bandages > 0 && status.currentHP < status.maxHP)
        {
            status.currentHP++;
            status.bandages--;
            _uI.BandagesOnHold(status.bandages);
            _uI.LifeBarState(status.currentHP);
        }

        if (Input.GetKeyDown(KeyCode.R) && status.batteries > 0)
        {
            flashLight.currentCharge = flashLight._maxBatteryCharge;
            status.batteries--;
            _uI.BatteriesOnHold(status.batteries);
        }
    }

}
