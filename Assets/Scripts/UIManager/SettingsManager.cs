using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [field: Header("------- Settings Manager -------")]
    [field: SerializeField] private GameObject settingsPanel;
    [field: SerializeField] private AudioSource musicAudioSource;
    [field: SerializeField] private AudioSource SoundAudioSource;
    [field: SerializeField] private Button settingsExitButton;
    [field: SerializeField] private GameObject soundUnmuteIcon;
    [field: SerializeField] private GameObject soundMuteIcon;
    [field: SerializeField] private GameObject musicUnmuteIcon;
    [field: SerializeField] private GameObject musicMuteIcon;
    [field: SerializeField] private GameObject vibrateEnableIcon;
    [field: SerializeField] private GameObject vibrateDisableIcon;
    [field: SerializeField] private GameObject restorePurchasesPanel;
    [field: SerializeField] private UIManager UIManager;


    private void Start()
    {
        UIManager = GetComponent<UIManager>();
        restorePurchasesPanel.SetActive(false);
        UpdateAudioSources();
    }
    public void UpdateIcons()
    {
        soundUnmuteIcon.SetActive(!SettingsDataHandler.instance.ReturnSavedValues().soundMuted);
        soundMuteIcon.SetActive(SettingsDataHandler.instance.ReturnSavedValues().soundMuted);

        musicUnmuteIcon.SetActive(!SettingsDataHandler.instance.ReturnSavedValues().musicMuted);
        musicMuteIcon.SetActive(SettingsDataHandler.instance.ReturnSavedValues().musicMuted);

        vibrateEnableIcon.SetActive(!SettingsDataHandler.instance.ReturnSavedValues().vibrationDisabled);
        vibrateDisableIcon.SetActive(SettingsDataHandler.instance.ReturnSavedValues().vibrationDisabled);

        UpdateAudioSources();
    }

    private void UpdateAudioSources()
    {
        if (SettingsDataHandler.instance.ReturnSavedValues().soundMuted)
        {
            SoundAudioSource.volume = 0f;
        }
        else
        {
            SoundAudioSource.volume = 1f;
        }
        if (SettingsDataHandler.instance.ReturnSavedValues().musicMuted)
        {
            musicAudioSource.volume = 0f;
        }
        else
        {
            musicAudioSource.volume = 1f;
        }
    }

    public void OnAudioButtonClicked(bool isMusic = false)
    {
        if (isMusic)
        {
            SettingsDataHandler.instance.ReturnSavedValues().musicMuted = !SettingsDataHandler.instance.ReturnSavedValues().musicMuted;
            SettingsDataHandler.instance.SaveSettingsData();
            UpdateIcons();
        }
        else
        {
            SettingsDataHandler.instance.ReturnSavedValues().soundMuted = !SettingsDataHandler.instance.ReturnSavedValues().soundMuted;
            SettingsDataHandler.instance.SaveSettingsData();
            UpdateIcons();
        }
    }

    public void OnVibrationButtonClick()
    {
        SettingsDataHandler.instance.ReturnSavedValues().vibrationDisabled = !SettingsDataHandler.instance.ReturnSavedValues().vibrationDisabled;
        if (!SettingsDataHandler.instance.ReturnSavedValues().vibrationDisabled)
        {
            Handheld.Vibrate();
        }
        SettingsDataHandler.instance.SaveSettingsData();
        UpdateIcons();
    }
    public void OnRestoreClicked()
    {
        settingsExitButton.interactable = false;
    }

    public void OnRestoreSuccessful()
    {
        settingsPanel.SetActive(false);
        UIManager.isOpen = false;
        restorePurchasesPanel.SetActive(true);
        settingsExitButton.interactable = true;
    }

    public void ExitRestorePurchases()
    {
        restorePurchasesPanel.SetActive(false);
    }
}
