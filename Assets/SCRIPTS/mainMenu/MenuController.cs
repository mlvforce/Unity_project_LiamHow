using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("New Game")]
    public string _newGameLevel;

    [Header("Level Select")]
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private GameObject noSavedGameDialog = null;

    private const string HighestUnlockedKey = "HighestUnlockedLevel";
    private const string MasterVolumeKey = "MasterVolume";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;

        LoadVolume();
        UpdateLevelButtons();
    }

    public void NewGameDialogYes()
    {
        PlayerPrefs.SetInt(HighestUnlockedKey, 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        int highestUnlocked = PlayerPrefs.GetInt(HighestUnlockedKey, 1);

        if (highestUnlocked <= 1)
        {
            if (noSavedGameDialog != null)
                noSavedGameDialog.SetActive(true);

            return;
        }

        UpdateLevelButtons();
    }

    public void LoadLevel(int buildIndex)
    {
        int highestUnlocked = PlayerPrefs.GetInt(HighestUnlockedKey, 1);

        if (buildIndex <= highestUnlocked)
        {
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            if (noSavedGameDialog != null)
                noSavedGameDialog.SetActive(true);
        }
    }

    private void UpdateLevelButtons()
    {
        int highestUnlocked = PlayerPrefs.GetInt(HighestUnlockedKey, 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int sceneIndex = i + 1;

            levelButtons[i].interactable = sceneIndex <= highestUnlocked;
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Setvolume(float volume)
    {
        AudioListener.volume = volume;

        if (volumeTextValue != null)
            volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, AudioListener.volume);
        PlayerPrefs.Save();

        StartCoroutine(ConfirmationBox());
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(MasterVolumeKey, defaultVolume);

        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
            volumeSlider.value = savedVolume;

        if (volumeTextValue != null)
            volumeTextValue.text = savedVolume.ToString("0.0");
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;

            if (volumeSlider != null)
                volumeSlider.value = defaultVolume;

            if (volumeTextValue != null)
                volumeTextValue.text = defaultVolume.ToString("0.0");

            VolumeApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        if (comfirmationPrompt != null)
            comfirmationPrompt.SetActive(true);

        yield return new WaitForSeconds(2);

        if (comfirmationPrompt != null)
            comfirmationPrompt.SetActive(false);
    }
}