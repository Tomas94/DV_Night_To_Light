using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_State : MonoBehaviour
{
    [Header("Referencia UI")]
    [SerializeField] UI_Player uIPlayer;

    [Header("Stats")]
    //public int currentHP;
    public int cantidadBaterias;
    public int cantidadVendajes;

    [Header("Timer Nictofobia")]
    public bool isNicto;
    float startTime = 0f;
    float endTime = 5f;

    [Header("Linterna")]
    [SerializeField] Linterna linterna;

    private void Start()
    {
        isNicto = false;
        //maxHP = 3;
        //currentHP = maxHP;
        cantidadBaterias = 0;
        cantidadVendajes = 0;
    }

    private void Update()
    {
        //uIPlayer.LifeBarState(currentHP);
        ObjectUse();
        IsScared(isNicto);

        if (Input.GetKeyDown(KeyCode.K))
        {
            linterna.currentCharge = linterna.maxCharge;
           // currentHP++;        
        }
    }

    public void ObjectUse()
    {
        //Heal();
        ChangeBattery();
    }

    /*void Heal()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentHP < maxHP && cantidadVendajes > 0)
        {
            currentHP++;
            cantidadVendajes--;
            uIPlayer.BandagesOnHold(cantidadVendajes);
        }
    }*/

    public void ChangeBattery()
    {
        if (Input.GetKeyDown(KeyCode.R) && cantidadBaterias > 0)
        {
            linterna.currentCharge = linterna.maxCharge;
            linterna.bateriaSlider.currentCharge = linterna.currentCharge;
            cantidadBaterias--;
            uIPlayer.BatteriesOnHold(cantidadBaterias);
        }
    }

    void IsScared(bool nicto)
    {
        if (nicto)
        {
            startTime += Time.deltaTime;
            if (startTime >= endTime)
            {
                startTime = 0f;
                //TakeDamage();
            }
            Debug.Log("A Oscuras");
        }
        else
        {
            startTime = 0.0f;
            Debug.Log("En la Luz");
        }
    }

    /*public override void TakeDamage()
    {
        base.TakeDamage();
        
        if (currentHP <= 0)
        {
            StartCoroutine(GameOver());
        }
    }*/

    IEnumerator GameOver()
    {
        CharacterController playerCH = GetComponent<CharacterController>();
        playerCH.enabled = false;
        CanvasGroup fadeScreen = GetComponentInChildren<CanvasGroup>();
        float timeToFade = 0;
        float fadeDuration = 0.5f;

        while(timeToFade < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, timeToFade / fadeDuration);
            fadeScreen.alpha = alpha;
            timeToFade += Time.deltaTime;
            yield return null;
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        fadeScreen.alpha = 1;
        SceneManager.LoadScene("Moriste");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LuzTrigger")
        {
            isNicto = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "LuzTrigger")
        {
            if(!linterna.isLightOn) isNicto = true;
        }
    }
}
