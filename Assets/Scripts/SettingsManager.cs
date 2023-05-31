using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public Button previousLanguageButton;
    public Button nextLanguageButton;

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
