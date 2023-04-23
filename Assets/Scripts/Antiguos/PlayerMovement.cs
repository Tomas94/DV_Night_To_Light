using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public enum MovementState { walking, sprinting, crouching, air }
    public MovementState _playerState;

    [Header("References")]
    [Tooltip("Camera Orientation")]
    [SerializeField] Transform _orientation; //Orientation
    Rigidbody _rb;
    
    [Header("Inputs & Direction")]
    float _horizontalInput;
    float _verticalInput;
    Vector3 _moveDirection;

    [Header("Speed")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _sprintSpeed;
    [SerializeField] float _crouchSpeed;
    
    [Header("Booleans")]
    [SerializeField] bool _isCrouch;
    [SerializeField] bool _isGrounded;

    [Header("Action Keys")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    Vector3 playerSize;
    [SerializeField]float groundDrag;
    LayerMask whatIsGround;
        
    private void Start()
    {       
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        playerSize = transform.localScale;
        whatIsGround = LayerMask.GetMask("Floor");
    }

    private void Update()
    {     
        transform.localScale = playerSize;
       
        Myinputs();
        SpeedControl();
        GroundDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Myinputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (_isGrounded)
        {
            CrouchControl();
            SprintControl();
        }      
    }

    private void CrouchControl()
    {
        //Start Crouch
        if (Input.GetKeyDown(crouchKey))
        {
            playerSize.y = 0.5f;
            _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            _crouchSpeed = _walkSpeed * 0.7f;
            _moveSpeed = _crouchSpeed;
            _playerState = MovementState.crouching;
            _isCrouch = true;
        }

        //Crouching
        /*if (Input.GetKey(crouchKey))
        {
            _crouchSpeed = _walkSpeed * 0.7f;
            _moveSpeed = _crouchSpeed;
            _playerState = MovementState.crouching;
        }*/
        
        //End Crouch
        if (Input.GetKeyUp(crouchKey) && _isCrouch)
        {
            _moveSpeed = _walkSpeed;
            playerSize.y = 1f;
            _isCrouch = false;
        }
    }

    private void SprintControl()
    {
        if (!_isCrouch)
        {
            //Sprint
            if (Input.GetKey(sprintKey))
            {
                _sprintSpeed = _walkSpeed * 1.8f;
                _moveSpeed = _sprintSpeed;
                _playerState = MovementState.sprinting;
            }
            //Walk
            else
            {
                _moveSpeed = _walkSpeed;
                _playerState = MovementState.walking;
            }
        }
        
    }

    private void GroundDrag()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y + 0.7f, whatIsGround);
        if (_isGrounded) { _rb.drag = groundDrag;}
        else { _rb.drag = 0;}
    }
   
    private void MovePlayer()
    {
        //Calculate movement direction
        float rbAceleration = 10f;
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        _rb.AddForce(_moveDirection.normalized * (_moveSpeed * rbAceleration), ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(_rb.velocity.x, 0f, _rb.velocity.z);

        //Limit velocity if needed
        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }
}
