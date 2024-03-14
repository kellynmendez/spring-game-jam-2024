using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildManager : StationManager
{
    [Header("Weapons")]
    [SerializeField] GameObject swordPrefab;
    [SerializeField] GameObject helmetPrefab;
    [SerializeField] GameObject arrowPrefab;

    [Header("UI Objects")]
    [SerializeField] GameObject chooseMoldsScreen;
    [SerializeField] GameObject swordUnfilledMold;
    [SerializeField] GameObject helmetUnfilledMold;
    [SerializeField] GameObject arrowUnfilledMold;
    [SerializeField] GameObject swordFilledMold;
    [SerializeField] GameObject helmetFilledMold;
    [SerializeField] GameObject arrowFilledMold;

    private BuildState currentState;
    private GameObject unfilledMold = null;
    private GameObject filledMold = null;
    private GameObject weaponObj = null;
    private StationUtils station;

    private enum BuildState
    {
        Inactive,
        ChoosingMold,
        FillingMold
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        gameScreen.SetActive(false);
        currentState = BuildState.Inactive;
        station = GetComponent<StationUtils>();
    }

    public override void StartGame()
    {
        inactive = false;
        currentState = BuildState.ChoosingMold;
        gameScreen.SetActive(true);
        chooseMoldsScreen.SetActive(true);
    }

    public void SwordChosen()
    {
        unfilledMold = swordUnfilledMold;
        filledMold = swordFilledMold;
        weaponObj = Instantiate(swordPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        SwitchToFillMold();
    }

    public void HelmetChosen()
    {
        unfilledMold = helmetUnfilledMold;
        filledMold = helmetFilledMold;
        weaponObj = Instantiate(helmetPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        SwitchToFillMold();
    }

    public void ArrowChosen()
    {
        unfilledMold = arrowUnfilledMold;
        filledMold = arrowFilledMold;
        weaponObj = Instantiate(arrowPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        SwitchToFillMold();
    }

    private void SwitchToFillMold()
    {
        chooseMoldsScreen.SetActive(false);
        unfilledMold.SetActive(true);
        currentState = BuildState.FillingMold;
    }

    public void FillMold()
    {
        unfilledMold.SetActive(false);
        filledMold.SetActive(true);
        StartCoroutine(WaitAfterFill());
    }

    IEnumerator WaitAfterFill()
    {
        yield return new WaitForSeconds(1f);

        // Resetting UI
        weaponObj.SetActive(true);
        filledMold.SetActive(false);
        // Setting build state
        currentState = BuildState.Inactive;

        ExitGame();
        yield break;
    }
}
