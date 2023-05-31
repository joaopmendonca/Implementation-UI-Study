using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables

    private bool startWasPressed = false;

    public AudioController audioController;

    [Header("UI Elements")]
    public GameObject pressStartObj;
    public GameObject mainWindow;
    public GameObject configureWindow;
    public GameObject loadingScreen;

    [Header("Main Menu Elements")]
    public Button[] mainMenuButtons;

    [Header("Scene Music Clips")]
    public AudioClip mainTheme;

    [Header("Audio Clips")]
    public AudioClip pressStartSound;
    public AudioClip clickSound;
    public AudioClip confirmSound;
    public AudioClip cancelSound;

    #endregion

    private void Start()
    {     
        if (mainWindow != null)
        {
            audioController.playMusic(mainTheme);
        }
    }

    private void Update()
    {
        // Verifica se o bot�o Start foi pressionado
        if (!startWasPressed && Input.GetButtonDown("Submit"))
        {
            PressStartButton();
        }
    }

    #region Button Click Methods

    // M�todo chamado quando o bot�o Start � pressionado
    public void PressStartButton()
    {
        startWasPressed = true;
        StartCoroutine(PressStartEffect());
    }

    // Efeito visual quando o bot�o Start � pressionado
    public IEnumerator PressStartEffect()
    {
        audioController.playSoundFx(pressStartSound);

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.25f);
            pressStartObj.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            pressStartObj.SetActive(true);
        }

        pressStartObj.SetActive(false);

        // Abre o menu principal
        OpenMainMenu();
        audioController.playSoundFx(confirmSound);
    }

    // Abre o menu principal
    public void OpenMainMenu()
    {
        mainWindow.SetActive(true);

        // Chama a anima��o do menu principal
        Animator mainMenuAnimator = mainWindow.GetComponent<Animator>();
        if (mainMenuAnimator != null)
        {
            mainMenuAnimator.SetBool("mainMenuON",true);
            EnableMainMenuButtons();
        }
    }
        
    // Fecha o menu principal e abre uma janela espec�fica
    public void CloseMainMenuAndOpenWindow(GameObject window)
    {       
        DisableMainMenuButtons();
        // Chama a anima��o de desativar do menu principal
        Animator mainMenuAnimator = mainWindow.GetComponent<Animator>();
        if (mainMenuAnimator != null)
        {
            mainMenuAnimator.SetBool("mainMenuON",false);
        }

        StartCoroutine(OpenWindowAfterAnimation(window));
    }

    // Espera a anima��o de desativar do menu principal terminar e abre a janela espec�fica
    private IEnumerator OpenWindowAfterAnimation(GameObject window)
    {
        yield return new WaitForSeconds(1f); // Tempo de espera para a anima��o de desativar do menu principal

        mainWindow.SetActive(false);
        OpenWindow(window);
    }

    // Abre uma janela espec�fica
    public void OpenWindow(GameObject window)
    {     
        window.SetActive(true);
    }

    // Fecha uma janela espec�fica
    public void CloseWindow(GameObject window)
    {       
        window.SetActive(false);
    }

    public void EnableMainMenuButtons()
    {
        foreach(Button button in mainMenuButtons) 
        {
            button.interactable = true;
        }
    }

    public void DisableMainMenuButtons() 
    { 
        foreach(Button button in mainMenuButtons) 
        {
            button.interactable = false;
        } 
    }

    IEnumerator LoadingScreenFadeIn(float duration)
    {
        Image tempImage = loadingScreen.GetComponent<Image>();

        Color originalColor = tempImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f); // Cor alvo com alpha m�ximo (completamente vis�vel)

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Interpola��o entre 0 e 1 (progresso do efeito)

            tempImage.color = Color.Lerp(originalColor, targetColor, t); // Interpola��o da cor entre original e alvo

            yield return null; // Aguarda um frame antes de continuar a pr�xima itera��o do loop
        }

        tempImage.color = targetColor; // Define a cor final para garantir que seja exatamente a cor alvo (totalmente vis�vel)
    }

    public void OpenLoadingScreen(float duration)
    {
        loadingScreen.SetActive(true);

        StartCoroutine(LoadingScreenFadeIn(duration));
    }

    #endregion

    #region Other Methods

    // Encerra o jogo
    public void QuitGame()
    {
        // Exemplo: Adicione qualquer l�gica adicional antes de sair do jogo
        Application.Quit();
    }

    #endregion
}
    