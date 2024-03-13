using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PACPointer))]
public class PlayerSM : MonoBehaviour
{
    [SerializeField] public GameObject weaponHolder;

    [SerializeField] private BuildManager buildManager;
    [SerializeField] private AssembleManager assembleManager;
    [SerializeField] private PaintManager paintManager;
    [SerializeField] private MoldManager moldManager;

    [HideInInspector] public bool carryingWeapon = false;

    private PlayerState currentPlayerState;
    private PACPointer movementComp;
    private GameObject weapon = null;

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
                movementComp.enabled = true;
                break;
            case PlayerState.Build:
                buildManager.StartGame();
                break;
            case PlayerState.Assemble:
                assembleManager.StartGame();
                break;
            case PlayerState.Paint:
                paintManager.StartGame();
                break;
            case PlayerState.Mold:
                moldManager.StartGame();
                break;
            default:
                Debug.Log("Error: State doesn't exist");
                break;
        }
    }

    public GameObject GetWeapon()
    {
        return weapon;
    }

    public void SetWeapon(GameObject weapon)
    {
        this.weapon = weapon;
    }
}
