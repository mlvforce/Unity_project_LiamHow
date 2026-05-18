using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FP_Movment : MonoBehaviour
{

    //creating the header for the players speed values 
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;  
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float crouchSpeed = 2f;
    //adding the fields for different player speeds 

    [Header("References")]
    [SerializeField] private Transform cameraTransform; //getting the cameras facing loaction 
    [SerializeField] private InputActionReference moveAction; //intput action reference for the move action 

    private CharacterController _characterController; // getting reference for the charater controller
    private Vector2 _moveInput;   //storing the move input 

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }


    private void OnEnable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;

    }

    private void OnDisable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;

    }

    private void StoreMovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var move  = cameraTransform.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y)).normalized;
        var currentSpeed = walkSpeed; //adding the speed variations
        var finalMove =move * currentSpeed;


        _characterController.Move(finalMove * Time.deltaTime);
    }
}
