using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDataHandler : MonoBehaviour
{
    public static SettingsDataHandler instance;
    [field: Header("------- Settings Data Handler -------")]
    public SettingsData settingsData;

    private void Awake()
    {
        instance = this;

        settingsData = SaveLoadManager.LoadData<SettingsData>();
        if (settingsData == null)
        {
            settingsData = new SettingsData();
        }
    }

    #region Return All Data
    public SettingsData ReturnSavedValues()
    {
        return settingsData;
    }
    #endregion

    #region Saving
    public void SaveSettingsData()
    {
        SaveLoadManager.SaveData(settingsData);
    }
    private void OnDisable()
    {
        SaveSettingsData();
    }
    #endregion
}

[System.Serializable]
public class SettingsData
{
    public bool soundMuted = false;
    public bool musicMuted = false;
    public bool vibrationDisabled = false;
}

