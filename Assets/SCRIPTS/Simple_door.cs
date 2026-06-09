using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Simple_door : MonoBehaviour
{
    [SerializeField] private List<Simple_toggable> requiredButtons;
    [SerializeField] private List<PressurePlate> requiredPressurePlates;

    [SerializeField] private UnityEvent onDoorOpen;
    [SerializeField] private UnityEvent onDoorClose;
    [SerializeField] private Animator doorAnimator;

    private bool isOpen;
    private bool puzzleCompleted;

    private void Update()
    {
        if (puzzleCompleted) return;

        bool allRequirementsMet = true;

        foreach (Simple_toggable button in requiredButtons)
        {
            if (button == null || !button.IsOpen)
            {
                allRequirementsMet = false;
                break;
            }
        }

        if (allRequirementsMet)
        {
            foreach (PressurePlate plate in requiredPressurePlates)
            {
                if (plate == null || !plate.IsOpen)
                {
                    allRequirementsMet = false;
                    break;
                }
            }
        }

        if (allRequirementsMet && !isOpen)
        {
            isOpen = true;
            doorAnimator.SetTrigger("OpenDoor");
            onDoorOpen.Invoke();
        }
        else if (!allRequirementsMet && isOpen)
        {
            isOpen = false;
            doorAnimator.SetTrigger("CloseDoor");
            onDoorClose.Invoke();
        }
    }

    public void CompletePuzzle()
    {
        puzzleCompleted = true;
        isOpen = false;
        doorAnimator.SetTrigger("CloseDoor");
        onDoorClose.Invoke();
    }
}