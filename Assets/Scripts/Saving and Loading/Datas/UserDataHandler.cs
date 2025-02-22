using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UserDataHandler : MonoBehaviour
{
    public static UserDataHandler instance;
    [SerializeField] private UserData userData;

    private void Awake()
    {
        instance = this;
        userData = SaveLoadManager.LoadData<UserData>();

        if (userData == null)
        {
            userData = new UserData();
        }
    }

    public void UpdateReviewRequested()
    {
        userData.hasRequestedReview = true;
    }

    #region Return All Data
    public UserData ReturnSavedValues()
    {
        return userData;
    }
    #endregion

    #region Saving
    public void SaveUserData()
    {
        SaveLoadManager.SaveData(userData);
    }
    private void OnDisable()
    {
        SaveUserData();
    }
    #endregion
}

[System.Serializable]
public class UserData
{
    public int numberOfWins = 0;
    public int numberOfLosses = 0;
    public bool hasRequestedReview = false;
    public int shortestMatch= 100;
    public int longestMatch = 0;
}
