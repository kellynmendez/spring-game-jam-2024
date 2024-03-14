using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildManager : StationManager
{
    [Header("Mold Breaking Settings")]
    [SerializeField] int numSwordsToMoldBreak = 5;
    [SerializeField] int numHelmetsToMoldBreak = 5;
    [SerializeField] int numArrowsToMoldBreak = 5;

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

    [Header("Broken Mold UI")]
    [SerializeField] GameObject brokenMoldGroup;
    [SerializeField] GameObject swordBrokenMold;
    [SerializeField] GameObject helmetBrokenMold;
    [SerializeField] GameObject arrowBrokenMold;

    private BuildState currentState;
    private GameObject unfilledMold = null;
    private GameObject filledMold = null;
    private GameObject weaponObj = null;
    private StationUtils station;
    private int swordCounter;
    private int helmetCounter;
    private int arrowCounter;

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
        swordCounter = numSwordsToMoldBreak;
        helmetCounter = numHelmetsToMoldBreak;
        arrowCounter = numArrowsToMoldBreak;
    }

    public override void StartGame()
    {
        inactive = false;
        currentState = BuildState.ChoosingMold;
        gameScreen.SetActive(true);
        brokenMoldGroup.SetActive(false);
        chooseMoldsScreen.SetActive(true);
    }

    public void SwordChosen()
    {
        swordCounter--;
        unfilledMold = swordUnfilledMold;
        filledMold = swordFilledMold;
        weaponObj = Instantiate(swordPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        SwitchToFillMold();
    }

    public void HelmetChosen()
    {
        swordCounter--;
        unfilledMold = helmetUnfilledMold;
        filledMold = helmetFilledMold;
        weaponObj = Instantiate(helmetPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        SwitchToFillMold();
    }

    public void ArrowChosen()
    {
        arrowCounter--;
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
        if (swordCounter == 0) 
        {
            swordCounter = numSwordsToMoldBreak;
            StartCoroutine(BreakMold(filledMold, swordUnfilledMold, swordBrokenMold));
        }
        else if (helmetCounter == 0)
        {
            helmetCounter = numHelmetsToMoldBreak;
            StartCoroutine(BreakMold(filledMold, helmetUnfilledMold, helmetBrokenMold));
        }
        else if (arrowCounter == 0)
        {
            arrowCounter = numArrowsToMoldBreak;
            StartCoroutine(BreakMold(filledMold, arrowUnfilledMold, arrowBrokenMold));
        }
        else
        {
            StartCoroutine(WaitAfterFill());
        }
    }

    IEnumerator BreakMold(GameObject filledMold, GameObject unbrokenMold, GameObject brokenMold)
    {
        yield return new WaitForSeconds(1f);
        filledMold.SetActive(false);
        brokenMoldGroup.SetActive(true);
        unbrokenMold.SetActive(true);
        brokenMold.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        unbrokenMold.SetActive(false);
        brokenMold.SetActive(true);
        StartCoroutine(WaitAfterFill());
        yield break;
    }

    IEnumerator WaitAfterFill()
    {
        yield return new WaitForSeconds(1f);

        // Resetting UI
        brokenMoldGroup.SetActive(false);
        weaponObj.SetActive(true);
        filledMold.SetActive(false);
        // Setting build state
        currentState = BuildState.Inactive;

        ExitGame();
        yield break;
    }
}
