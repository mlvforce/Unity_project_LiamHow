using UnityEngine;

public class Door_close_behind : MonoBehaviour
{
    // calling the "isOpen" class so code can be clsoed by another script

    [SerializeField] private Simple_door doorToClose;

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        doorToClose.CompletePuzzle();
    }



}
