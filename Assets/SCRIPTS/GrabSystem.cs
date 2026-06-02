using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabSystem : MonoBehaviour
{
    [Header("Grab Settings")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float moveForce = 12f;
    [SerializeField] private float maxGrabDistance = 4f;
    [SerializeField] private InputActionReference throwAction;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody _heldRigidbody;

    private void FixedUpdate()
    {
        if (_heldRigidbody == null)
        {
            return;
        }

        Vector3 directionToHoldPoint = holdPoint.position - _heldRigidbody.position;

        if (directionToHoldPoint.magnitude > maxGrabDistance)
        {
            Drop();
            return;
        }

        _heldRigidbody.linearVelocity = directionToHoldPoint * moveForce;
    }

    public void ToggleGrab(Rigidbody rigidbodyToGrab)
    {
        if (_heldRigidbody != null)
        {
            Drop();
            return;
        }

        Grab(rigidbodyToGrab);
    }
    private void OnEnable()
    {
        throwAction.action.performed += ThrowObject;
    }

    private void OnDisable()
    {
        throwAction.action.performed -= ThrowObject;
    }
    private void ThrowObject(InputAction.CallbackContext context)
    {
        if (_heldRigidbody == null)
        {
            return;
        }
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is missing on Grabsystem");
            return;
        }
        Rigidbody rb = _heldRigidbody;

        Drop();

        rb.linearVelocity = cameraTransform.forward * throwForce;
    }


    private void Grab(Rigidbody rigidbodyToGrab)
    {
        _heldRigidbody = rigidbodyToGrab;
        _heldRigidbody.useGravity = false;
        _heldRigidbody.linearDamping = 8f;
        _heldRigidbody.angularDamping = 8f;
    }

    private void Drop()
    {
        _heldRigidbody.useGravity = true;
        _heldRigidbody.linearDamping = 0f;
        _heldRigidbody.angularDamping = 0.05f;
        _heldRigidbody = null;
    }
}