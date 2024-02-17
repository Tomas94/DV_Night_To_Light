using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
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

    //public Animator animator;

    void Start()
    {
        walkingSpeed = 12f;
        //_movSpeed = walkingSpeed;
        //runSpeed = _movSpeed * 1.4f;
       // crouchSpeed = _movSpeed * 0.7f;
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

        //Movement();
    }

    /*public override void Movement()
    {
        base.Movement();
        Walk();
        Run();
        Crouch();
        Fall();
    }*/

    #region Funciones Relacionadas al Movimiento


    public void Walk()
    {
        //if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) { PlayerState((int)playerState.Idle); }
        if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) /*&& !Input.GetKey(KeyCode.LeftControl)*/)
        {
            //_movSpeed = walkingSpeed;
            //PlayerState((int)playerState.Walking);
        }
        //else { PlayerState((int)playerState.Idle); }
    }


    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) /*!isCrouch*/)
        {
            //_movSpeed = runSpeed;
            //PlayerState((int)playerState.Running);
        }
        //else { PlayerState((int)playerState.Walking); }
    }

    public void Crouch()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            //_movSpeed = crouchSpeed;
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

    /*enum playerState
    {
        Idle,
        Walking,
        Running
    }

    void PlayerState(int estado)
    {
        if (estado == 0)//IDLE
        {
            animator.SetBool("IDLE", true);
            animator.SetBool("WALK", false);
            animator.SetBool("RUN", false);
        }
        else if (estado == 1)
        {
            animator.SetBool("IDLE", false);
            animator.SetBool("WALK", true);
            animator.SetBool("RUN", false);
        }
        else
        {
            animator.SetBool("IDLE", false);
            animator.SetBool("WALK", false);
            animator.SetBool("RUN", true);
        }
    }*/


    #endregion


}
