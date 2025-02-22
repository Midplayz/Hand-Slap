using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: Header("------- Main Menu UI Manager -------")]
    [field: SerializeField] private GameObject mainMenuCanvas;
    [field: SerializeField] private GameObject inGameCanvas;
    [field: SerializeField] private GameObject settingsCanvas;
    [field: SerializeField] private GameObject profileCanvas;
    [field: SerializeField] private GameObject gameOverCanvas;
    [field: SerializeField] private AudioSource audioSource;
    [field: SerializeField] private GameFlowManager gameFlowManager;

    [field: Header("------- Script References -------")]
    //[field: SerializeField] private StopWatch stopWatch;
    [field: SerializeField] private ImageAnimation imageAnimation;
    private SettingsManager settingsManager;
    private ProfileManager profileManager;
    

    [field: HideInInspector] public bool isOpen = false;
    private void Start()
    {
        settingsManager = GetComponent<SettingsManager>();
        profileManager = GetComponent<ProfileManager>();
        Time.timeScale = 1.0f;
        mainMenuCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        profileCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }
    public void SettingsUIControl()
    {
        if (!isOpen)
        {
            settingsCanvas.SetActive(!isOpen);
            settingsManager.UpdateIcons();
            isOpen = true;
        }
        else if (isOpen)
        {
            settingsCanvas.SetActive(!isOpen);
            isOpen = false;
        }
    }

    public void ProfileUIControl()
    {
        if (!isOpen)
        {
            profileManager.SetPlayerStats();
            profileCanvas.SetActive(!isOpen);
            isOpen = true;
        }
        else if (isOpen)
        {
            profileCanvas.SetActive(!isOpen);
            isOpen = false;
        }
    }

    public void OpenLink(string urlLink)
    {
        Application.OpenURL(urlLink);
    }

    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        profileCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        //stopWatch.StartStopwatch(gameFlowManager.lengthOfRound);
        gameFlowManager.startTimer = true;
    }

    public void PostGameActions()
    {
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        profileCanvas.SetActive(false);
    }

    public void GoToMainMenu()
    {
        gameOverCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        imageAnimation.ResetEverything();
        inGameCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        profileCanvas.SetActive(false);
    }
}
