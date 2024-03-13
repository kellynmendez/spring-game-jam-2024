using Mono.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : StationManager
{
    [SerializeField] DropPoint[] swordSlots;

    private AssembleState currentState;
    private bool isSword = false;
    private bool isHelmet = false;
    private bool isArrow = false;

    private enum AssembleState
    {
        Inactive,
        Assembling
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        gameScreen.SetActive(false);
        currentState = AssembleState.Inactive;
    }

    public override void StartGame()
    {
        inactive = false;
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
            StartCoroutine(WaitAfterAssembled());
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


    IEnumerator WaitAfterAssembled()
    {
        yield return new WaitForSeconds(1f);
        foreach (DropPoint slot in swordSlots)
        {
            slot.ResetDropPoint();
        }
        currentState = AssembleState.Inactive;
        ExitGame();
        yield break;
    }
}
