using Mono.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : Station
{
    [SerializeField] DropPoint[] swordSlots;

    private AssembleState currentState;
    private bool isSword = false;
    private bool isHelmet = false;
    private bool isArrow = false;

    private enum AssembleState
    {
        Inactive,
        Assembling,
        Finished
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        gameScreen.SetActive(false);
        currentState = AssembleState.Inactive;
    }

    public override void StartGame()
    {
        currentState = AssembleState.Assembling;
        gameScreen.SetActive(true);
    }

    private void Update()
    {
        if (currentState == AssembleState.Inactive) 
            return;
        else if (currentState == AssembleState.Assembling)
        {
            HandleDragInput();
        }
        else if (currentState == AssembleState.Finished)
        {
            currentState = AssembleState.Inactive;
            foreach (DropPoint slot in swordSlots)
            {
                slot.ResetDropPoint();
            }
            ExitGame();
        }
    }

    private void HandleDragInput()
    {
        bool isAssembled = true;
        foreach (DropPoint slot in swordSlots)
        {
            if (slot.Filled() == false)
            {
                isAssembled = false;
                break;
            }
        }

        if (isAssembled)
        {
            currentState = AssembleState.Finished;
        }
    }

    public void SetIsSword(bool newIsSword)
    {
        isSword = newIsSword;
    }

    public void SetIsHelmet(bool newIsHelmet)
    {
        isHelmet = newIsHelmet;
    }

    public void SetIsArrow(bool newIsArrow)
    {
        isArrow = newIsArrow;
    }
}
