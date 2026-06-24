using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Things to freeze")]
    [SerializeField] private FP_Movment playerMovement;
    [SerializeField] private GrabSystem grabSystem;
    [SerializeField] private CinemachineInputAxisController cameraInput;

    [Header("Scene")]
    [SerializeField] private string mainMenuSceneName = "main_menu";

    private bool paused;

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (grabSystem != null)
            grabSystem.enabled = false;

        if (cameraInput != null)
            cameraInput.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        paused = false;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (grabSystem != null)
            grabSystem.enabled = true;

        if (cameraInput != null)
            cameraInput.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}