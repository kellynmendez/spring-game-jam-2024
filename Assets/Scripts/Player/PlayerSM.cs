using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PACPointer))]
public class PlayerSM : MonoBehaviour
{
    [SerializeField] public Transform weaponHolder;
    [HideInInspector] public bool carryingWeapon = false;

    private StationManager stationManager = null;

    private PlayerState currentPlayerState;
    private PACPointer movementComp;
    private PointAndClickMovement pacComp;
    private Weapon weapon = null;

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
        pacComp = this.transform.GetComponent<PointAndClickMovement>();
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

    public void ChangeState(PlayerState nextState, StationManager newStnMngr)
    {
        stationManager = newStnMngr;

        // Change state
        currentPlayerState = nextState;

        // Change to any of the stations
        if (currentPlayerState != PlayerState.CorePlay)
        {
            stationManager.StartGame();
            movementComp.enabled = false;
            pacComp.enabled = false;
        }
        else if (currentPlayerState == PlayerState.CorePlay)
        {
            movementComp.enabled = true;
            pacComp.enabled = true;
        }
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
