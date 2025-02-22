using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum PossibleOptions
{
    Slap,
    Dodge,
    Grab,
    NothingSelected
}
public class GameFlowManager : MonoBehaviour
{
    [field: Header("------- Main Game Flow -------")]
    [field: SerializeField] public int lengthOfRound { get; private set; }
    [field: SerializeField] private int timeBetweenRounds = 2;
    [field: SerializeField] private ActionBehaviour playerActionBehaviour;
    [field: SerializeField] private ActionBehaviour opponentActionBehaviour;

    [field: Header("------- Visual Feel -------")]
    [field: SerializeField] private TextMeshProUGUI timerText;
    [field: SerializeField] private TextMeshProUGUI roundText;
    [field: SerializeField] private TextMeshProUGUI finalResultText;
    [field: SerializeField] private SkinnedMeshRenderer playerHand;
    [field: SerializeField] private SkinnedMeshRenderer opponentHand;
    [field: SerializeField] private Material normalMat;
    [field: SerializeField] private Material hurtMat;
    [field: SerializeField] private GameObject moneyGameObject;
    [field: SerializeField] private List<GameObject> comicEffects;
    [field: SerializeField] private ImageAnimation imageAnimation;
    [field: SerializeField] private SoundEffectsController soundEffectsController;
    [field: SerializeField] private ScreenShake screenShake;

    [field: Header("------- Resetting -------")]
    [field: SerializeField] private List<Button> allButtons;
    [field: SerializeField] private UIManager uiManager;

    public float timerTime = 0f; //Public Because UIManager starts this timer.
    public bool startTimer = false;

    private bool playerWon = false;
    private bool opponentWon = false;

    private int currentRound = 1;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        roundText.text = currentRound.ToString();
        timerTime = lengthOfRound;
        timerText.text = lengthOfRound.ToString();
    }

    void Update()
    {
        if (startTimer)
        {
            timerTime -= Time.unscaledDeltaTime;
            timerText.text = Mathf.RoundToInt(timerTime).ToString();
            if (timerTime <= 0f)
            {
                startTimer = false;
                playerActionBehaviour.onRoundOver();
                opponentActionBehaviour.onRoundOver();
                for(int i = 0; i < allButtons.Count; i++)
                {
                    allButtons[i].interactable = false;
                    timerText.gameObject.SetActive(false);
                }
                ChangeMaterials();
                MatchResult();
            }
        }
    }
    private void ChangeMaterials()
    {
        if (playerActionBehaviour.actionPerformed == PossibleOptions.Slap && opponentActionBehaviour.actionPerformed == PossibleOptions.Grab)
        {
            soundEffectsController.PlaySlapSound();
            opponentHand.material = hurtMat;
            opponentActionBehaviour.hasToSkipTurn = true;
            screenShake.Shake();
            ComicEffectController();
        }
        else if (playerActionBehaviour.actionPerformed == PossibleOptions.Grab && opponentActionBehaviour.actionPerformed == PossibleOptions.Slap)
        {
            soundEffectsController.PlaySlapSound();
            screenShake.Shake();
            playerHand.material = hurtMat;
            playerActionBehaviour.hasToSkipTurn = true;
            ComicEffectController();
        }
        else if (playerActionBehaviour.actionPerformed == PossibleOptions.Dodge && opponentActionBehaviour.actionPerformed == PossibleOptions.Slap)
        {
            soundEffectsController.PlayDodgeSound();
            screenShake.Shake();
            opponentHand.material = hurtMat;
            opponentActionBehaviour.hasToSkipTurn = true;
            ComicEffectController();
        }

        else if (playerActionBehaviour.actionPerformed == PossibleOptions.Slap && opponentActionBehaviour.actionPerformed == PossibleOptions.Dodge)
        {
            soundEffectsController.PlaySlapSound();
            screenShake.Shake();
            playerHand.material = hurtMat;
            playerActionBehaviour.hasToSkipTurn = true;
            ComicEffectController();
        }
        else if (playerActionBehaviour.actionPerformed == PossibleOptions.Slap && opponentActionBehaviour.actionPerformed == PossibleOptions.Slap)
        {
            soundEffectsController.PlaySlapSound();
            screenShake.Shake();
            opponentHand.material = hurtMat;
            playerHand.material = hurtMat;
            opponentActionBehaviour.hasToSkipTurn = true;
            playerActionBehaviour.hasToSkipTurn = true;
            ComicEffectController();
        }
        else
        {
            opponentActionBehaviour.hasToSkipTurn = false;
            playerActionBehaviour.hasToSkipTurn = false;
            playerHand.material = normalMat;
            opponentHand.material = normalMat;
        }

        if (playerActionBehaviour.actionPerformed == PossibleOptions.Grab && opponentActionBehaviour.actionPerformed == PossibleOptions.Grab)
        {
            soundEffectsController.PlayClashSound();
        }
        else if (playerActionBehaviour.actionPerformed == PossibleOptions.Dodge && opponentActionBehaviour.actionPerformed == PossibleOptions.Dodge || playerActionBehaviour.actionPerformed == PossibleOptions.Dodge && opponentActionBehaviour.actionPerformed == PossibleOptions.NothingSelected)
        {
            soundEffectsController.PlayDodgeSound();
        }
    }
    private void MatchResult()
    {
        if (opponentActionBehaviour.actionPerformed == PossibleOptions.Grab && playerActionBehaviour.actionPerformed == PossibleOptions.Dodge || opponentActionBehaviour.actionPerformed == PossibleOptions.Grab && playerActionBehaviour.actionPerformed == PossibleOptions.NothingSelected)
        {
            opponentWon = true;
            soundEffectsController.PlayGrabSound();
            moneyGameObject.SetActive(false);
        }
        else if(playerActionBehaviour.actionPerformed == PossibleOptions.Grab && opponentActionBehaviour.actionPerformed == PossibleOptions.Dodge || playerActionBehaviour.actionPerformed == PossibleOptions.Grab && opponentActionBehaviour.actionPerformed == PossibleOptions.NothingSelected)
        {
            playerWon = true;
            soundEffectsController.PlayGrabSound();
            moneyGameObject.SetActive(false);
        }
        else if(playerActionBehaviour.actionPerformed == PossibleOptions.Dodge && opponentActionBehaviour.actionPerformed == PossibleOptions.NothingSelected)
        {
            soundEffectsController.PlayDodgeSound();
        }
        else if(playerActionBehaviour.actionPerformed == PossibleOptions.Slap && opponentActionBehaviour.actionPerformed == PossibleOptions.NothingSelected)
        {
            soundEffectsController.PlaySlapSound();
        }
        if(playerWon == true || opponentWon == true)
        {
            GameOverSequence();
        }
        else
        {
            playerActionBehaviour.actionPerformed = PossibleOptions.NothingSelected;
            opponentActionBehaviour.actionPerformed = PossibleOptions.NothingSelected;
            StartNextRound();
        }

    }
    private async void StartNextRound()
    {
        await Task.Delay(timeBetweenRounds * 1000);
        if (!playerActionBehaviour.hasToSkipTurn)
        {
            for (int i = 0; i < allButtons.Count; i++)
            {
                allButtons[i].interactable = true;
                allButtons[i].OnDeselect(null);
            }
        }

        currentRound++;
        roundText.text = currentRound.ToString();
        timerTime = lengthOfRound;
        timerText.text = lengthOfRound.ToString();
        timerText.gameObject.SetActive(true);
        startTimer = true;
        
    }
    private async void GameOverSequence()
    {
        UpdateSavedVariables();
        await Task.Delay(timeBetweenRounds * 1000);
        imageAnimation.SetImage(playerWon);
        if (playerWon)
        {
            finalResultText.text = "GREEN WINS!";
        }
        else
        {
            finalResultText.text = "GREEN LOSES!";
        }
        uiManager.PostGameActions();
        ResetGameValues();
    }

    private async void ComicEffectController()
    {
        int randomImage = Random.Range(0, comicEffects.Count);
        comicEffects[randomImage].SetActive(true);
        await Task.Delay(1000);
        comicEffects[randomImage].SetActive(false);
    }

    public void ResetGameValues()
    {
        currentRound = 1;
        roundText.text = currentRound.ToString();
        timerTime = lengthOfRound;
        timerText.gameObject.SetActive(true);
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].interactable = true;
            allButtons[i].OnDeselect(null);
        }
        moneyGameObject.SetActive(true);
        playerWon = false;
        opponentWon = false;
    }

    private void UpdateSavedVariables()
    {
        if (playerWon)
        {
            UserDataHandler.instance.ReturnSavedValues().numberOfWins++;
            if (currentRound > UserDataHandler.instance.ReturnSavedValues().longestMatch)
            {
                UserDataHandler.instance.ReturnSavedValues().longestMatch = currentRound;
            }
            if (currentRound < UserDataHandler.instance.ReturnSavedValues().shortestMatch)
            {
                UserDataHandler.instance.ReturnSavedValues().shortestMatch = currentRound;
            }
        }
        else
        {
            UserDataHandler.instance.ReturnSavedValues().numberOfLosses++;
        }

        UserDataHandler.instance.SaveUserData();
    }

}
