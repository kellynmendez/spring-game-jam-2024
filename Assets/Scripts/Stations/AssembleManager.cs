using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : Station
{
    [SerializeField] DropPoint swordHilt;
    [SerializeField] DropPoint swordBlade;

    private AssembleState currentState;

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
    }

    private void HandleDragInput()
    {

    }
}
