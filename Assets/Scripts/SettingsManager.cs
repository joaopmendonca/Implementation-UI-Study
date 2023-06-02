using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public event Action<int> LanguageChanged;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";
    private const string LanguageKey = "Language";

    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    public TMP_Dropdown languageDropdown;
    public int languageIndex;

    public Button saveButton;
    public Button cancelButton;

    public float defaultMusicVolume = 1f;
    public float defaultSoundVolume = 1f;

    private void Awake()
    {
        languageDropdown.onValueChanged.AddListener(OnLanguageDropdownValueChanged);
        Instance = this;
    }

    private void Start()
    {
        LoadSettings();
    }

    private void OnLanguageDropdownValueChanged(int index)
    {
        languageIndex = languageDropdown.value;
        LanguageChanged?.Invoke(languageIndex);
    }

    private void LoadSettings()
    {
        // Carregar as configura��es salvas do PlayerPrefs
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, defaultMusicVolume);
        float savedSoundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, defaultSoundVolume);
        int savedLanguageIndex = PlayerPrefs.GetInt(LanguageKey, 0);

        // Definir os valores carregados nos controles deslizantes de volume e no dropdown de idioma
        musicVolumeSlider.value = savedMusicVolume;
        soundVolumeSlider.value = savedSoundVolume;
        languageDropdown.value = savedLanguageIndex;

        // Atualizar a vari�vel languageIndex com o valor atual do dropdown de idioma
        languageIndex = languageDropdown.value;

        Debug.Log("Configura��es carregadas!");
    }

    public void SaveSettings()
    {
        // Salvar as configura��es atuais nos PlayerPrefs
        float musicVolume = musicVolumeSlider.value;
        float soundVolume = soundVolumeSlider.value;

        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SoundVolumeKey, soundVolume);
        PlayerPrefs.SetInt(LanguageKey, languageIndex);
        PlayerPrefs.Save();

        Debug.Log("Configura��es salvas!");
    }

    public void CancelSettings()
    {
        // Carregar as configura��es novamente para reverter as altera��es feitas pelo jogador
        LoadSettings();

        Debug.Log("Configura��es canceladas!");
    }
}
