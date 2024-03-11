using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PACPointer))]
public class PlayerSM : MonoBehaviour
{
    [SerializeField] BuildManager buildManager;
    [SerializeField] AssembleManager assembleManager;
    [SerializeField] PaintManager paintManager;
    [SerializeField] MoldManager moldManager;


    private PlayerState currentPlayerState;
    private PACPointer movementComp;

    public enum PlayerState
    {
        CorePlay,
        Build,
        Assemble,
        Paint,
        Mold
    }

    private void Awake()
    {
        movementComp = this.transform.GetComponent<PACPointer>();
    }

    private void Start()
    {
        // Setting starting game state
        currentPlayerState = PlayerState.CorePlay;
    }

    private void Update()
    {
        Debug.Log(currentPlayerState);
    }

    public void ChangeState(PlayerState nextState)
    {
        Debug.Log("Exiting " + nextState);

        // Disable movement if entering mini game
        if (currentPlayerState == PlayerState.CorePlay 
            && nextState != PlayerState.CorePlay)
        {
            movementComp.enabled = false;
        }

        // Change state
        currentPlayerState = nextState;

        // Start state
        switch (currentPlayerState)
        {
            // Enable movement component if exitng mini game
            case PlayerState.CorePlay:
                Debug.Log("Entering core play");
                movementComp.enabled = true;
                break;
            case PlayerState.Build:
                Debug.Log("Entering build");
                buildManager.StartGame();
                break;
            case PlayerState.Assemble:
                Debug.Log("Entering assemble");
                assembleManager.StartGame();
                break;
            case PlayerState.Paint:
                Debug.Log("Entering paint");
                paintManager.StartGame();
                break;
            case PlayerState.Mold:
                Debug.Log("Entering mold");
                moldManager.StartGame();
                break;
            default:
                Debug.Log("Error: State doesn't exist");
                break;
        }
    }
}
