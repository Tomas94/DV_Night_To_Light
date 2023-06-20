using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Movement : Entity
{
    [Header("Movimiento")]
    
    [SerializeField] CharacterController chController;
    [SerializeField] Transform groundCheck;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float walkingSpeed;
    [SerializeField] bool isCrouch;
    Vector3 fallingSpeedVector = Vector3.zero;



    void Start()
    {
        walkingSpeed = 12f;
        speed = walkingSpeed;
        runSpeed = speed * 1.4f;
        crouchSpeed = speed * 0.7f;
        isCrouch = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene("MenuInicial");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        Walk();
        Run();
        Crouch();
        Fall();
    }

    #region Funciones Relacionadas al Movimiento

    public void Walk()
        { if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) speed = walkingSpeed; }

    public void Run()
        { if (Input.GetKey(KeyCode.LeftShift) && !isCrouch) speed = runSpeed; }

    public void Crouch()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            isCrouch = true;
            chController.height = Mathf.Lerp(chController.height, 2f, 1 * Time.deltaTime );
        }
        else
        {
            isCrouch = false;
            chController.height = Mathf.Lerp(chController.height, 4f, 1 * Time.deltaTime );
        }
    }

    public void Fall()
    {
        float fallingSpeed = -2f;
        float gravity = -9.81f;
        float groundDistance = 0.4f;
        bool isGrounded;

        fallingSpeedVector.y += gravity * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, LayerMask.NameToLayer("Ground"));

        if (isGrounded && fallingSpeedVector.y < 0) fallingSpeedVector.y = fallingSpeed;

        chController.Move(fallingSpeedVector * Time.deltaTime);
    }

    #endregion


}
