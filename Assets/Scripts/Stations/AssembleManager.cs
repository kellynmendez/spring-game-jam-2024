using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleManager : StationManager
{
    [SerializeField] DropPoint3D[] swordSlots;
    [SerializeField] DropPoint3D[] helmetSlots;
    [SerializeField] DropPoint3D[] arrowSlots;
    [SerializeField] GameObject swordGroup;
    [SerializeField] GameObject helmetGroup;
    [SerializeField] GameObject arrowGroup;

    [SerializeField] AudioSource _assembled;
    [SerializeField] AudioClip _assembledClip;

    private AssembleState currentState;
    private Weapon stationWeapon = null;
    private DropPoint3D[] dropPoints = null;
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
        foreach (DropPoint3D slot in dropPoints)
        {
            if (slot.Filled() == false)
            {
                isAssembled = false;
                break;
            }
        }

        if (isAssembled && !finishedAssembly)
        {
            _assembled.clip = _assembledClip;
            _assembled.Play();
            finishedAssembly = true;
            StartCoroutine(WaitAfterAssembled());
        }
    }

    IEnumerator WaitAfterAssembled()
    {
        yield return new WaitForSeconds(1f);

        // Resetting UI
        foreach (DropPoint3D slot in dropPoints)
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
