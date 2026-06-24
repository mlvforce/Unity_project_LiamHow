using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private const string HighestUnlockedKey = "HighestUnlockedLevel";

    [Header("Scene Loading")]
    [SerializeField] private bool loadNextSceneAutomatically = true;
    [SerializeField] private string sceneToLoad;

    [Header("Final Level")]
    [SerializeField] private bool isFinalLevel = false;
    [SerializeField] private string mainMenuSceneName = "main_menu";

    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (!other.CompareTag("Player")) return;

        hasTriggered = true;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (isFinalLevel)
        {
            SceneManager.LoadScene(mainMenuSceneName);
            return;
        }

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        int highestUnlocked = PlayerPrefs.GetInt(HighestUnlockedKey, 1);

        if (nextScene > highestUnlocked)
        {
            PlayerPrefs.SetInt(HighestUnlockedKey, nextScene);
            PlayerPrefs.Save();
        }

        if (loadNextSceneAutomatically)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}