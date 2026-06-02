using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;
    private GrabSystem _grabSystem;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _grabSystem = FindFirstObjectByType<GrabSystem>();
    }

    public void Interact()
    {
        _grabSystem.ToggleGrab(_rigidbody);
    }
}