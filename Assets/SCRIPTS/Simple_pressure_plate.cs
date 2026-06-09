using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private bool isOpen;

    [SerializeField] private UnityEvent onOpen;
    [SerializeField] private UnityEvent onClose;

    public bool IsOpen => isOpen;

    private int objectsOnPlate = 0;

    private void OnTriggerEnter(Collider other)
    {
        bool validObject =
            other.attachedRigidbody != null ||
            other.GetComponent<CharacterController>() != null ||
            other.CompareTag("Player");

        if (!validObject) return;

        objectsOnPlate++;

        if (!isOpen)
        {
            isOpen = true;
            onOpen.Invoke();
            Debug.Log(gameObject.name + " pressure plate ON");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool validObject =
            other.attachedRigidbody != null ||
            other.GetComponent<CharacterController>() != null ||
            other.CompareTag("Player");

        if (!validObject) return;

        objectsOnPlate--;

        if (objectsOnPlate <= 0)
        {
            objectsOnPlate = 0;
            isOpen = false;
            onClose.Invoke();
            Debug.Log(gameObject.name + " pressure plate OFF");
        }
    }
}