using UnityEngine;
using UnityEngine.Events;

public class Simple_Button : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent onInteract;


    [ContextMenu("Interact")]
    public void Interact() // implamenting the IInteractable method
    {
        onInteract.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
