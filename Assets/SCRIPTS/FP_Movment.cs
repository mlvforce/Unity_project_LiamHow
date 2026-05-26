using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class FP_Movment : MonoBehaviour
{

    //creating the header for the players speed values 
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;  
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float crouchSpeed = 2f;
    //adding the fields for different player speeds 

    //adding gravity to the player charater 
    [Header("Jump and Fall")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float initialFallVeclocity = -2f;


    [Header("Crouching")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private float crouchTransitionSpeed = 10f;
    [SerializeField] private float cameraOffset = 0.4f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform; //getting the cameras facing loaction 
    [SerializeField] private InputActionReference moveAction; //intput action reference for the move action 
    [SerializeField] private InputActionReference jumpAction; // input action for jumping
    [SerializeField] private InputActionReference crouchAction; // input reference for crouching 
    [SerializeField] private InputActionReference sprintAction; // reference for sprinting

    private CharacterController _characterController; // getting reference for the charater controller
    private Vector2 _moveInput;   //storing the move input 

    private bool _isGrounded;
    private bool _isRunning;
    private bool _isCrouching;
    private float _verticalVelcoity;
    private float _targetHeight;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _targetHeight = standingHeight;
    }


    private void OnEnable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;
        jumpAction.action.performed += Jump;
        sprintAction.action.performed += Sprint;
        sprintAction.action.canceled += Sprint;
        crouchAction.action.performed += Crouch;
        


    }

    private void OnDisable()
    {
        moveAction.action.performed -= StoreMovementInput;
        moveAction.action.canceled -= StoreMovementInput;
        jumpAction.action.performed -= Jump;
        sprintAction.action.performed -= Sprint;
        sprintAction.action.canceled -= Sprint;
        crouchAction.action.performed -= Crouch;
    



    }

  

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _characterController.isGrounded;
        HandleGravity();
        HandleMovement();
        HandleCrouchTransition();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _verticalVelcoity = jumpForce;
        }
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        if (_isCrouching)
        {
            if (!CantStandUp())
            {
                return;
            }
            _targetHeight = standingHeight;
            _isCrouching = false; 
        }
        else
        {
            _targetHeight = crouchingHeight;
            _isCrouching = true;

        }
            
    }

    private bool CantStandUp()
    {
        return !Physics.CapsuleCast(
            transform.position + _characterController.center,
            transform.position + (Vector3.up * (_characterController.height / 2)),
            _characterController.radius,
            Vector3.up,
            1f, // small check distance
            ~0,
            QueryTriggerInteraction.Ignore
        );
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        _isRunning = context.performed;
    }



    private void StoreMovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }



    private void HandleGravity()
    {
        if( _isGrounded && _verticalVelcoity <0 )
        {
            _verticalVelcoity = initialFallVeclocity;
        }
        _verticalVelcoity += gravity * Time.deltaTime;
    }
    private void HandleMovement()
    {
        var move  = cameraTransform.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y)).normalized;
        var currentSpeed = _isCrouching ? crouchSpeed : _isRunning ? runSpeed: walkSpeed; //adding the speed variations
        var finalMove =move * currentSpeed;
        finalMove.y = _verticalVelcoity;

       var collisions = _characterController.Move(finalMove * Time.deltaTime); // checking the above collisions to make sure that if true number is 0
        if ((collisions & CollisionFlags.Above) != 0)
        {
            _verticalVelcoity = initialFallVeclocity;
        }

    }

    private void HandleCrouchTransition()
    {
        var currentHeight = _characterController.height;
        if(Mathf.Abs(currentHeight - _targetHeight) < 0.01f)
        {
            _characterController.height = _targetHeight;
            return; 
        }
        var newHeight = Mathf.Lerp(currentHeight, _targetHeight,crouchTransitionSpeed * Time.deltaTime);
        _characterController.height = newHeight;
        _characterController.center = Vector3.up * (newHeight * 0.5f);

        var cameraTargetPosition = cameraTransform.localPosition;
        cameraTargetPosition.y = _targetHeight - cameraOffset;
        cameraTransform.localPosition = Vector3.Lerp(
            cameraTransform.localPosition, cameraTargetPosition,
            crouchTransitionSpeed * Time.deltaTime);
    }
}
