using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [field: Header("------- Profile & Statistics -------")]
    [field: SerializeField] private TextMeshProUGUI numberOfWins;
    [field: SerializeField] private TextMeshProUGUI numberOfLosses;
    [field: SerializeField] private TextMeshProUGUI winPercentage;
    [field: SerializeField] private TextMeshProUGUI shortestMatch;
    [field: SerializeField] private TextMeshProUGUI longestMatch;

    public void SetPlayerStats()
    {
        numberOfWins.text = UserDataHandler.instance.ReturnSavedValues().numberOfWins.ToString();
        numberOfLosses.text = UserDataHandler.instance.ReturnSavedValues().numberOfLosses.ToString();
        winPercentage.text = CalculateWinPercentage() + "%";
        shortestMatch.text = UserDataHandler.instance.ReturnSavedValues().shortestMatch + " ROUNDS";
        longestMatch.text = UserDataHandler.instance.ReturnSavedValues().longestMatch + " ROUNDS";
    }
    private float CalculateWinPercentage()
    {
        float wins = UserDataHandler.instance.ReturnSavedValues().numberOfWins;
        float losses = UserDataHandler.instance.ReturnSavedValues().numberOfLosses;
        return Mathf.Round(((wins / (wins + losses)) * 100)*10) /10;
    }
}
