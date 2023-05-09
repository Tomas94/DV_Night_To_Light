using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PMovement : MonoBehaviour
{

    Transform groundCheck;
    CharacterController chController;
    PlayerStats stats = new PlayerStats(12f,16f,6f,false,2,5);

    Vector3 fallingSpeed = Vector3.zero;
    
    void Start()
    {
        chController = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
    }

    void Update()
    {
        Movement(CurrentSpeed());
        CrouchState();
        FallCheck();
    }

    void Movement(float speed)
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 Direction = transform.right *hInput + transform.forward * vInput;
        Direction.Normalize();
        chController.Move(Direction * speed * Time.deltaTime);
    }

    float CurrentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            stats.isCrouch = true;
            return stats.crouchSpeed;
        }
        stats.isCrouch = false;

        if (Input.GetKey(KeyCode.LeftShift) && !stats.isCrouch) return stats.runSpeed;
        else return stats.speed;
    }

    void CrouchState()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            chController.height = stats.crouchHeigth;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            chController.height = stats.standHeight;
        }
    }

    void FallCheck()
    {
        float groundDistance = 0.4f;
        bool isGrounded;
      
        fallingSpeed.y += stats.gravity * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance , LayerMask.NameToLayer("Ground"));
        
        if (isGrounded && fallingSpeed.y < 0) fallingSpeed.y = -2f;

        chController.Move(fallingSpeed * Time.deltaTime);
    }
}
