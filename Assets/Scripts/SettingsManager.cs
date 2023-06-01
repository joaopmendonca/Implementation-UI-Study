using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";
    private const string LanguageKey = "Language";

    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    public TextMeshProUGUI languageText;

    public Button saveButton;
    public Button cancelButton;
    public Button previousButton;
    public Button nextButton;

    public float defaultMusicVolume = 0.5f;
    public float defaultSoundVolume = 0.5f;

    public enum DefaultLanguage
    {
        Portuguese,
        English,
        Spanish
    }

    public DefaultLanguage defaultLanguage = DefaultLanguage.English;

    private float savedMusicVolume;
    private float savedSoundVolume;

    private int currentLanguageIndex = 0;
    private string[] languages = { "Português do Brasil", "English", "Español" };
    private string currentLanguage = "";

    private void Start()
    {
        LoadSettings();
        InitializeUI();
    }

    private void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(MusicVolumeKey))
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, defaultMusicVolume);
        }

        if (!PlayerPrefs.HasKey(SoundVolumeKey))
        {
            PlayerPrefs.SetFloat(SoundVolumeKey, defaultSoundVolume);
        }

        if (!PlayerPrefs.HasKey(LanguageKey))
        {
            SetDefaultLanguage();
        }
    }

    private void SetDefaultLanguage()
    {
        switch (defaultLanguage)
        {
            case DefaultLanguage.Portuguese:
                PlayerPrefs.SetString(LanguageKey, "Português do Brasil");
                break;
            case DefaultLanguage.English:
                PlayerPrefs.SetString(LanguageKey, "English");
                break;
            case DefaultLanguage.Spanish:
                PlayerPrefs.SetString(LanguageKey, "Español");
                break;
        }
    }

    private void InitializeUI()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, defaultMusicVolume);
        soundVolumeSlider.value = PlayerPrefs.GetFloat(SoundVolumeKey, defaultSoundVolume);

        saveButton.onClick.AddListener(OnSaveButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        savedMusicVolume = musicVolumeSlider.value;
        savedSoundVolume = soundVolumeSlider.value;

        previousButton.onClick.AddListener(OnPreviousButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);

        currentLanguage = PlayerPrefs.GetString(LanguageKey);
        UpdateLanguageText();
    }

    private void UpdateLanguageText()
    {
        languageText.text = currentLanguage;
    }

    private void OnPreviousButtonClick()
    {
        currentLanguageIndex--;
        if (currentLanguageIndex < 0)
            currentLanguageIndex = languages.Length - 1;

        currentLanguage = languages[currentLanguageIndex];
        UpdateLanguageText();
    }

    private void OnNextButtonClick()
    {
        currentLanguageIndex++;
        if (currentLanguageIndex >= languages.Length)
            currentLanguageIndex = 0;

        currentLanguage = languages[currentLanguageIndex];
        UpdateLanguageText();
    }

    private void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(SoundVolumeKey, soundVolumeSlider.value);
        PlayerPrefs.SetString(LanguageKey, currentLanguage);

        PlayerPrefs.Save();
        Debug.Log("Preferences saved successfully!");
    }

    private void OnCancelButtonClick()
    {
        musicVolumeSlider.value = savedMusicVolume;
        soundVolumeSlider.value = savedSoundVolume;
    }
}
