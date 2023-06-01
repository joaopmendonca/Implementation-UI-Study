using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    #region Variables

    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";

    [Header("Sliders")]
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    [Header("Language")]
    public TextMeshProUGUI languageText;
    public Button previousButton;
    public Button nextButton;

    private int currentLanguageIndex = 0;
    private string[] languages = { "Português do Brasil", "English", "Español" };
    private string currentLanguage = "";

    [Header("Buttons")]
    public Button saveButton;
    public Button cancelButton;

    [Header("Default Values")]
    public float defaultMusicVolume = 0.5f;
    public float defaultSoundVolume = 0.5f;

    private float savedMusicVolume;
    private float savedSoundVolume;

    #endregion

    #region Unity Methods

    private void Start()
    {
        LoadSettings();
        InitializeUI();
    }

    #endregion

    #region Settings Methods

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
    }

    private void InitializeUI()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, defaultMusicVolume);
        soundVolumeSlider.value = PlayerPrefs.GetFloat(SoundVolumeKey, defaultSoundVolume);

        saveButton.onClick.AddListener(OnSaveButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        // Salva as preferências atuais para poder reverter caso o jogador cancele
        savedMusicVolume = musicVolumeSlider.value;
        savedSoundVolume = soundVolumeSlider.value;

        previousButton.onClick.AddListener(OnPreviousButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);

        UpdateLanguageText();
    }

    private void UpdateLanguageText()
    {
        currentLanguage = languages[currentLanguageIndex];
        languageText.text = currentLanguage;
    }

    private void OnPreviousButtonClick()
    {
        currentLanguageIndex--;
        if (currentLanguageIndex < 0)
            currentLanguageIndex = languages.Length - 1;

        UpdateLanguageText();
    }

    private void OnNextButtonClick()
    {
        currentLanguageIndex++;
        if (currentLanguageIndex >= languages.Length)
            currentLanguageIndex = 0;

        UpdateLanguageText();
    }

    #endregion

    #region Button Click Event Methods

    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(SoundVolumeKey, soundVolumeSlider.value);

        PlayerPrefs.Save();
        Debug.Log("Preferências salvas com sucesso!");
    }

    private void OnCancelButtonClick()
    {
        musicVolumeSlider.value = savedMusicVolume;
        soundVolumeSlider.value = savedSoundVolume;
    }

    #endregion
}
