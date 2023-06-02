using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UITranslator : MonoBehaviour
{
    public TextAsset xmlFile;
    public string identifier;
    public TMPro.TextMeshProUGUI textMeshProUGUI;
    private string columnName;

    private void Start()
    {
        SettingsManager.Instance.LanguageChanged += OnLanguageChanged;
        UpdateColumnName();
        textMeshProUGUI.text = XMLReader.Instance.GetTranslation(xmlFile, identifier, columnName);
    }

    private void OnLanguageChanged(int languageIndex)
    {
        UpdateColumnName();
        textMeshProUGUI.text = XMLReader.Instance.GetTranslation(xmlFile, identifier, columnName);       
    }    

    private void UpdateColumnName()
    {
        switch (SettingsManager.Instance.languageIndex)
        {
            case 0:
                columnName = "English";
                break;
            case 1:
                columnName = "Portuguese";
                break;
            case 2:
                columnName = "Spanish";
                break;
            default:
                columnName = "English";
                break;
        }
    }
}
