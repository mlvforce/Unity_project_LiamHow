using UnityEngine;

public class CursorControll : MonoBehaviour
{
    //locking cursor in place and removing the (pc cursor)

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

}
