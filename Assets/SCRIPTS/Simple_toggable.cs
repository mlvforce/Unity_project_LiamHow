using UnityEngine;
using UnityEngine.Events;

public class Simple_toggable : MonoBehaviour,IInteractable
{

    [SerializeField] private bool isOpen;
    [SerializeField] private UnityEvent onOpen; // getting two different fields to be able to toggle interactions

    [SerializeField] private UnityEvent onClose;

    public bool IsOpen => isOpen;
    public void Interact() //calling the interactble
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            onOpen.Invoke();
        }
        else
        {
            onClose.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
