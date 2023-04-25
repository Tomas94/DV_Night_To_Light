using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Movimiento")]


    [SerializeField] PlayerInventory inventario;
    [SerializeField] CharacterController controller;
    [SerializeField] FlashlightScript1 flashlight;
    [SerializeField] UI_Player playerUI;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource vida2;
    [SerializeField] AudioSource vida1;
    Nictofobia nictofobia;

    public float speed = 12f;
    public float runSpeed = 16f;
    public float crouchSpeed = 6f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    [SerializeField] bool isGrounded;

    [Header("Stats")]

    public int maxHP;
    public int currentHP;
    public int bats;
    public int cures;

    Pickable pickables;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        maxHP = 3;
        currentHP = maxHP;
        bats = 0;
        cures = 0;
    }


    void Update()
    {
        Movement();
        ResourcesUpdate();
        ObjectUse();

        playerUI.CureState(cures);

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage();

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            inventario.totalBaterias++;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventario.totalVendajes++;
        }

    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) controller.Move(move * runSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.LeftControl)) controller.Move(move * crouchSpeed * Time.deltaTime);
        else controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void ResourcesUpdate()
    {
        bats = inventario.totalBaterias;
        cures = inventario.totalVendajes;
    }

    void ObjectUse()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cures > 0 && currentHP < maxHP)
        {
            currentHP++;
            inventario.totalVendajes--;
            LifeBarStatus(currentHP);
        }

        if (Input.GetKeyDown(KeyCode.R) && bats != 0)
        {
            flashlight.currentCharge = flashlight._maxBatteryCharge;
            inventario.totalBaterias--;
        }
    }

    /*public void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E)) animator.SetBool("Abierta", true);
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pickeable")
        {
            pickables = other.GetComponent<Pickable>();

            if (pickables.isVisible)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickables.PickupObject(other.name);
                    Destroy(other.gameObject);
                }
            }
           
        }

        /*if(other.tag == "Puerta")
        {
            other.GetComponent<Animator>().SetBool("Abierta", true);
        }*/
    }

    public void TakeDamage()
    {
        currentHP--;
        LifeBarStatus(currentHP);

        if (currentHP == maxHP)
        {
           
        }
        if (currentHP == 2)
        {
          
        }
        if (currentHP == 1)
        {

        }


        if (currentHP <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Moriste");
        }
    }

    void LifeBarStatus(int vida)
    {
        playerUI.LifeBar(currentHP);
    }
}
