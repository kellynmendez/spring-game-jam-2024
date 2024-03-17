using System;
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
    [HideInInspector] public bool swordMoldActive = true;
    [HideInInspector] public bool helmetMoldActive = true;
    [HideInInspector] public bool arrowMoldActive = true;

    [Header("Weapon Prefabs")]
    [SerializeField] GameObject swordPrefab;
    [SerializeField] GameObject helmetPrefab;
    [SerializeField] GameObject arrowPrefab;

    [Header("Choose Mold UI Objects")]
    [SerializeField] GameObject chooseMoldsScreen;
    [SerializeField] GameObject swordChooseMold;
    [SerializeField] GameObject helmetChooseMold;
    [SerializeField] GameObject arrowChooseMold;
    [SerializeField] GameObject swordGrayedOutMold;
    [SerializeField] GameObject helmetGrayedOutMold;
    [SerializeField] GameObject arrowGrayedOutMold;

    [Header("Animation")]
    [SerializeField] Animator anim;
    private const string IDLE_ANIM = "Idle";
    private const string SWORD_POUR_ANIM = "SwordPour";
    private const string HELMET_POUR_ANIM = "HelmetPour";
    private const string ARROW_POUR_ANIM = "ArrowPour";

    [Header("Fill Mold UI Objects")]
    [SerializeField] GameObject swordUnfilledMold;
    [SerializeField] GameObject helmetUnfilledMold;
    [SerializeField] GameObject arrowUnfilledMold;
    [SerializeField] GameObject swordFilledMold;
    [SerializeField] GameObject helmetFilledMold;
    [SerializeField] GameObject arrowFilledMold;

    [Header("Broken Mold UI")]
    [SerializeField] GameObject brokenMoldGroup;
    [SerializeField] GameObject swordUnbrokenMold;
    [SerializeField] GameObject helmetUnbrokenMold;
    [SerializeField] GameObject arrowUnbrokenMold;
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
    private MoldManager moldManager;

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
        moldManager = FindObjectOfType<MoldManager>();
    }

    public override void StartGame()
    {
        inactive = false;
        currentState = BuildState.ChoosingMold;
        gameScreen.SetActive(true);
        brokenMoldGroup.SetActive(false);
        chooseMoldsScreen.SetActive(true);

        // Decide if sword can be made
        if (swordMoldActive)
        {
            swordChooseMold.SetActive(true);
            swordGrayedOutMold.SetActive(false);
        }
        else
        {
            swordChooseMold.SetActive(false);
            swordGrayedOutMold.SetActive(true);
        }
        // Decide if helmet can be made
        if (helmetMoldActive)
        {
            helmetChooseMold.SetActive(true);
            helmetGrayedOutMold.SetActive(false);
        }
        else
        {
            helmetChooseMold.SetActive(false);
            helmetGrayedOutMold.SetActive(true);
        }
        // Decide if arrow can be made
        if (arrowMoldActive)
        {
            arrowChooseMold.SetActive(true);
            arrowGrayedOutMold.SetActive(false);
        }
        else
        {
            arrowChooseMold.SetActive(false);
            arrowGrayedOutMold.SetActive(true);
        }
    }

    public void SwordChosen()
    {
        swordCounter--;
        //unfilledMold = swordUnfilledMold;
        //filledMold = swordFilledMold;
        weaponObj = Instantiate(swordPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        chooseMoldsScreen.SetActive(false);
        currentState = BuildState.FillingMold;

        anim.Play(SWORD_POUR_ANIM);
        StartCoroutine(WaitAfterFill(()=>IsAnimated(anim, SWORD_POUR_ANIM)));
    }

    public void HelmetChosen()
    {
        helmetCounter--;
        //unfilledMold = helmetUnfilledMold;
        //filledMold = helmetFilledMold;
        weaponObj = Instantiate(helmetPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        chooseMoldsScreen.SetActive(false);
        currentState = BuildState.FillingMold;

        anim.Play(HELMET_POUR_ANIM);
        StartCoroutine(WaitAfterFill(()=>IsAnimated(anim, HELMET_POUR_ANIM)));
    }

    public void ArrowChosen()
    {
        arrowCounter--;
        //unfilledMold = arrowUnfilledMold;
        //filledMold = arrowFilledMold;
        weaponObj = Instantiate(arrowPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        chooseMoldsScreen.SetActive(false);
        currentState = BuildState.FillingMold;

        anim.Play(ARROW_POUR_ANIM);
        StartCoroutine(WaitAfterFill(()=>IsAnimated(anim, ARROW_POUR_ANIM)));
    }

    private void SwitchToFillMold()
    {
        chooseMoldsScreen.SetActive(false);
        //unfilledMold.SetActive(true);
        currentState = BuildState.FillingMold;
    }

    public void FillMold()
    {
        unfilledMold.SetActive(false);
        filledMold.SetActive(true);
        if (swordCounter == 0) 
        {
            swordMoldActive = false;
            swordCounter = numSwordsToMoldBreak;
            moldManager.canMakeSwordMold = true;
            StartCoroutine(BreakMold(filledMold, swordUnbrokenMold, swordBrokenMold));
        }
        else if (helmetCounter == 0)
        {
            helmetMoldActive = false;
            helmetCounter = numHelmetsToMoldBreak;
            moldManager.canMakeHelmetMold = true;
            StartCoroutine(BreakMold(filledMold, helmetUnbrokenMold, helmetBrokenMold));
        }
        else if (arrowCounter == 0)
        {
            arrowMoldActive = false;
            arrowCounter = numArrowsToMoldBreak;
            moldManager.canMakeArrowMold = true;
            StartCoroutine(BreakMold(filledMold, arrowUnbrokenMold, arrowBrokenMold));
        }
        else
        {
            //StartCoroutine(WaitAfterFill());
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
        //StartCoroutine(WaitAfterFill());
        yield break;
    }

    IEnumerator WaitAfterFill(Func<bool> condition)
    {
        yield return new WaitForEndOfFrame();

        while (condition())
            yield return null;

        // Resetting UI
        //brokenMoldGroup.SetActive(false);
        //swordBrokenMold.SetActive(false);
        //helmetBrokenMold.SetActive(false);
        //arrowBrokenMold.SetActive(false);
        //filledMold.SetActive(false);

        weaponObj.SetActive(true);
        currentState = BuildState.Inactive;

        ExitGame();
        yield break;
    }

    public bool IsAnimated(Animator ani, string animationName, int layerIndex = 0)
    {
        return ani.GetCurrentAnimatorStateInfo(layerIndex).IsName(animationName);
    }
}
