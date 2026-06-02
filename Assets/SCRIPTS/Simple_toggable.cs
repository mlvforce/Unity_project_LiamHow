using UnityEngine;
using UnityEngine.Events;

public class Simple_toggable : MonoBehaviour,IInteractable
{

    [SerializeField] private bool isOpen;
    [SerializeField] private UnityEvent onOpen; // getting two different fields to be able to toggle interactions

    [SerializeField] private UnityEvent onClose;

    public void Interact() // calling the IInteractable
    {
        if (isOpen)
        {
            onClose.Invoke();
        }
        else
        {
            onOpen.Invoke();
        }
        isOpen = !isOpen;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
