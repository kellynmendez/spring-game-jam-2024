using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : StationManager
{
    [SerializeField] DropPoint[] swordSlots;
    [SerializeField] DropPoint[] helmetSlots;
    [SerializeField] DropPoint[] arrowSlots;
    [SerializeField] GameObject swordGroup;
    [SerializeField] GameObject helmetGroup;
    [SerializeField] GameObject arrowGroup;

    private AssembleState currentState;
    private Weapon stationWeapon = null;
    private DropPoint[] dropPoints = null;
    private bool finishedAssembly = false;

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
        stationWeapon = GetComponent<StationUtils>().weaponAtStation;
        finishedAssembly = false;

        if (stationWeapon is Sword)
        {
            dropPoints = swordSlots;
            swordGroup.SetActive(true);
            helmetGroup.SetActive(false);
            arrowGroup.SetActive(false);
        }
        else if (stationWeapon is Helmet)
        {
            dropPoints = helmetSlots;
            swordGroup.SetActive(false);
            helmetGroup.SetActive(true);
            arrowGroup.SetActive(false);
        }
        else if (stationWeapon is Arrow)
        {
            dropPoints = arrowSlots;
            swordGroup.SetActive(false);
            helmetGroup.SetActive(false);
            arrowGroup.SetActive(true);
        }
    }

    private void Update()
    {
        if (currentState == AssembleState.Inactive) 
            return;
        else if (currentState == AssembleState.Assembling)
        {
            if (dropPoints != null)
            {
                HandleDragInput();
            }
        }
    }

    private void HandleDragInput()
    {
        bool isAssembled = true;
        foreach (DropPoint slot in dropPoints)
        {
            if (slot.Filled() == false)
            {
                isAssembled = false;
                break;
            }
        }

        if (isAssembled && !finishedAssembly)
        {
            finishedAssembly = true;
            StartCoroutine(WaitAfterAssembled());
        }
    }

    IEnumerator WaitAfterAssembled()
    {
        yield return new WaitForSeconds(1f);

        // Resetting UI
        foreach (DropPoint slot in dropPoints)
        {
            slot.ResetDropPoint();
        }
        dropPoints = null;
        // Setting weapon state and showing fully assembled weapon
        stationWeapon.currentState = Weapon.WeaponState.Assembled;
        stationWeapon.ShowDefault();
        stationWeapon = null;
        // Changing assemble state
        currentState = AssembleState.Inactive;

        ExitGame();
        yield break;
    }
}
