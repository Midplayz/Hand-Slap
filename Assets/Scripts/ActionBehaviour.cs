using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionBehaviour : MonoBehaviour
{
    [field: Header("------- Basic Action Variables -------")]
    [field: SerializeField] private float moveSpeed = 2f;
    [field: SerializeField] private float duration = 2f;
    [field: SerializeField] private Vector3 movementOffset;
    [field: SerializeField] private Transform targetObject;
    [field: SerializeField] private GameObject lineRendererObject;
    [field: SerializeField] private Animator animator;

    [field: Header("------- Do Not Assign In Inspector -------")]
    public bool hasToSkipTurn = false;
    public PossibleOptions actionPerformed;

    [field: Header("------- For Player -------")]
    [field: SerializeField] private GameFlowManager gameFlowManager;

    [field: Header("------- For AI -------")]
    [field: SerializeField] private bool isAi = false;
    [field: SerializeField] private ActionBehaviour playerActionBehaviour;
    [field: SerializeField] private GameObject actionDonePopUp;
    [field: SerializeField] private Image actionDonePopUpImage;
    [field: SerializeField] private Sprite slapIcon;
    [field: SerializeField] private Sprite dodgeIcon;
    [field: SerializeField] private Sprite grabIcon;

    private Vector3 initialLocalPosition;
    private float elapsedTime = 0f;
    private bool movingForward = true;
    private bool movingBackward = false;
    private bool startMoving = false;

    private void Start()
    {
        initialLocalPosition = targetObject.localPosition;
        animator = GetComponent<Animator>();
        if(isAi) 
        { 
            actionDonePopUp.SetActive(false);
        }
    }

    private void Update()
    {
        if (startMoving)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / duration;

            if (movingForward)
            {
                targetObject.localPosition = Vector3.Lerp(initialLocalPosition, initialLocalPosition - movementOffset, progress);
            }
            else if(movingBackward)
            {
                targetObject.localPosition = Vector3.Lerp(initialLocalPosition  - movementOffset, initialLocalPosition, progress);
            }

            if (progress >= 1f)
            {
                if (movingBackward == false)
                {
                    movingForward = false;
                    movingBackward = true;
                    elapsedTime = 0f; 
                }
                else if(movingBackward == true)
                {
                    movingForward = true;
                    movingBackward = false;
                    elapsedTime = 0f; 
                    startMoving = false;
                }
            }
        }
    }

    public void onRoundOver()
    {
        if(isAi && !hasToSkipTurn || !isAi && !hasToSkipTurn && actionPerformed == PossibleOptions.NothingSelected)
        {
            PossibleOptions[] enumValues = (PossibleOptions[])Enum.GetValues(typeof(PossibleOptions));
            int randomIndex = UnityEngine.Random.Range(0, enumValues.Length-1);
            actionPerformed = enumValues[randomIndex];
        }

        if(isAi && playerActionBehaviour.hasToSkipTurn)
        {
            actionPerformed = PossibleOptions.Grab;
        }

        if (!hasToSkipTurn)
        {
            if (actionPerformed == PossibleOptions.Slap)
            {
                OnSlap();
                if (!SettingsDataHandler.instance.ReturnSavedValues().vibrationDisabled)
                {
                    Handheld.Vibrate();
                }
            }
            else if (actionPerformed == PossibleOptions.Dodge)
            {
                OnDodge();
            }
            else
            {
                OnGrab();
            }
        }

        if(hasToSkipTurn)
        {
            actionPerformed = PossibleOptions.NothingSelected;
        }
        if(isAi && actionPerformed != PossibleOptions.NothingSelected)
        {
            ActionDonePopup();
        }
    }

    private void OnSlap()
    {
        animator.SetTrigger("Slap");
        startMoving = true;
        //lineRendererObject.SetActive(true);
    }

    private void OnDodge()
    {
        animator.SetTrigger("Dodge");
        startMoving = true;
    }

    private void OnGrab()
    {
        animator.SetTrigger("Grab");
        startMoving = true;
    }

    public void SelectedButton(int ID)
    {
        if(ID == 0)
        {
        actionPerformed = PossibleOptions.Slap;
        }
        else if(ID == 1)
        {
        actionPerformed = PossibleOptions.Dodge;
        }
        else if(ID == 2)
        {
        actionPerformed = PossibleOptions.Grab;
        }
        else
        {
            Debug.LogError("Wrong ID Given To A Button");
        }
        gameFlowManager.timerTime = 0f;
    }

    private async void ActionDonePopup() //For AI
    {
        if(actionPerformed == PossibleOptions.Grab)
        {
            actionDonePopUpImage.sprite = grabIcon;
        }
        else if (actionPerformed == PossibleOptions.Dodge)
        {
            actionDonePopUpImage.sprite = dodgeIcon;
        }
        else if (actionPerformed == PossibleOptions.Slap)
        {
            actionDonePopUpImage.sprite = slapIcon;
        }
        actionDonePopUp.SetActive(true);
        await Task.Delay(1000);
        actionDonePopUp.SetActive(false);
    }
}
