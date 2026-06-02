using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class Interaction_controller : MonoBehaviour
{
    private Transform _cameraTransform;
    private IInteractable _currentInteractable;   


    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private InputActionReference interactionAction;  //simple settings for interactions



    private void Awake()
    {

        _cameraTransform = Camera.main.transform;

    }
    // Update is called once per frame
    void Update()
    {
        DectectInteractable();


    }

    private void OnEnable()
    {
        interactionAction.action.performed += HandleInteraction;

    }


    private void OnDisable()
    {
        interactionAction.action.performed -= HandleInteraction;

    }

    private void HandleInteraction(InputAction.CallbackContext context)
    {
        if (_currentInteractable != null) 
        
        {
            _currentInteractable.Interact();
        }


    }

    private void DectectInteractable()  //defining and creating the ray cast for the player to interact
    {
        var ray = new Ray(_cameraTransform.position, _cameraTransform.forward );
        IInteractable detectedInteractable = null; 
        if(Physics.Raycast(ray, out var hit, interactionDistance, interactionLayer))
        {
            hit.collider.TryGetComponent(out detectedInteractable);
        }
        if (_currentInteractable != detectedInteractable)
        {
            _currentInteractable = detectedInteractable;
        }
    }

}
