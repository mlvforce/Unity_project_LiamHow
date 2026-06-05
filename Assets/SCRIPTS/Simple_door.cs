using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Simple_door : MonoBehaviour
{
    [SerializeField] private List<Simple_toggable> requiredButtons;

    [SerializeField] private UnityEvent onDoorOpen;
    [SerializeField] private UnityEvent onDoorClose;

    private bool isOpen;
    public void ForceCloseDoor()
    {
        isOpen = false;
        onDoorClose.Invoke();

    }

    private void Update()
    {
        bool allButtonsOn = true;

        foreach (Simple_toggable button in requiredButtons)
        {
            if (button == null || !button.IsOpen)
            {
                allButtonsOn = false;
                break;
            }
        }
   
        if (allButtonsOn && !isOpen)
        {
            isOpen = true;
            onDoorOpen.Invoke();
        }
        else if (!allButtonsOn && isOpen)
        {
            isOpen = false;
            onDoorClose.Invoke();
        }
    }
}