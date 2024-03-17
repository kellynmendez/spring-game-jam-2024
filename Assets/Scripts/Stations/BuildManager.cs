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
    [SerializeField] Animator pourAnimator;
    private const string SWORD_POUR_ANIM = "SwordPour";
    private const string HELMET_POUR_ANIM = "HelmetPour";
    private const string ARROW_POUR_ANIM = "ArrowPour";
    [SerializeField] Animator swordBreakAnimator;
    private const string SWORD_BREAK_ANIM = "SwordBreak";
    private const string SWORD_BROKEN_ANIM = "SwordBroken";
    [SerializeField] Animator helmetBreakAnimator;
    private const string HELMET_BREAK_ANIM = "HelmetBreak";
    private const string HELMET_BROKEN_ANIM = "HelmetBroken";
    [SerializeField] Animator arrowBreakAnimator;
    private const string ARROW_BREAK_ANIM = "ArrowBreak";
    private const string ARROW_BROKEN_ANIM = "ArrowBroken";

    [Header("Broken Mold UI")]
    [SerializeField] GameObject animSwordBrokenMold;
    [SerializeField] GameObject animHelmetBrokenMold;
    [SerializeField] GameObject animArrowBrokenMold;
    [SerializeField] GameObject swordBrokenMold;
    [SerializeField] GameObject helmetBrokenMold;
    [SerializeField] GameObject arrowBrokenMold;
    [SerializeField] GameObject moldBrokenText;

    [Header("Sound")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _pickMold;
    [SerializeField] AudioClip _pourMold;
    [SerializeField] AudioClip _moldBreak;

    private BuildState currentState;
    private GameObject weaponObj = null;
    private StationUtils station;
    private int swordCounter;
    private int helmetCounter;
    private int arrowCounter;
    private GameObject animBrokenMold = null;
    private GameObject brokenMold = null;
    private MoldManager moldManager;
    private MoldInv moldInv;


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

        animSwordBrokenMold.SetActive(false);
        animHelmetBrokenMold.SetActive(false);
        animArrowBrokenMold.SetActive(false);
        swordBrokenMold.SetActive(false);
        helmetBrokenMold.SetActive(false);
        arrowBrokenMold.SetActive(false);
        moldBrokenText.SetActive(false);

        moldInv = FindObjectOfType<MoldInv>();
    }

    public override void StartGame()
    {
        inactive = false;
        currentState = BuildState.ChoosingMold;
        gameScreen.SetActive(true);
        chooseMoldsScreen.SetActive(true);
        animBrokenMold = null;
        brokenMold = null;

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
        _audioSource.clip = _pickMold;
        _audioSource.Play();

        weaponObj = Instantiate(swordPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        chooseMoldsScreen.SetActive(false);

        currentState = BuildState.FillingMold;

        pourAnimator.Play(SWORD_POUR_ANIM);

        _audioSource.clip = _pourMold;
        _audioSource.Play();

        StartCoroutine(WaitAfterFill(() => IsAnimated(pourAnimator, SWORD_POUR_ANIM)));
    }

    public void HelmetChosen()
    {
        helmetCounter--;
        _audioSource.clip = _pickMold;
        _audioSource.Play();

        weaponObj = Instantiate(helmetPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        chooseMoldsScreen.SetActive(false);

        currentState = BuildState.FillingMold;

        pourAnimator.Play(HELMET_POUR_ANIM);

        _audioSource.clip = _pourMold;
        _audioSource.Play();

        StartCoroutine(WaitAfterFill(() => IsAnimated(pourAnimator, HELMET_POUR_ANIM)));
    }

    public void ArrowChosen()
    {
        arrowCounter--;
        _audioSource.clip = _pickMold;
        _audioSource.Play();

        weaponObj = Instantiate(arrowPrefab);
        station.SetStationOccupied(true, weaponObj.GetComponent<Weapon>());
        weaponObj.SetActive(false);
        chooseMoldsScreen.SetActive(false);

        currentState = BuildState.FillingMold;

        pourAnimator.Play(ARROW_POUR_ANIM);

        _audioSource.clip = _pourMold;
        _audioSource.Play();

        StartCoroutine(WaitAfterFill(() => IsAnimated(pourAnimator, ARROW_POUR_ANIM)));
    }

    public bool IsAnimated(Animator ani, string animationName, int layerIndex = 0)
    {
        return ani.GetCurrentAnimatorStateInfo(layerIndex).IsName(animationName);
    }

    IEnumerator WaitAfterFill(Func<bool> condition)
    {
        yield return new WaitForEndOfFrame();

        while (condition())
            yield return null;

        CheckBreakMold();
        
        yield break;
    }

    private void CheckBreakMold()
    {
        if (swordCounter == 0)
        {
            swordMoldActive = false;
            swordCounter = numSwordsToMoldBreak;
            moldManager.canMakeSwordMold = true;
            moldInv.SetSwordInactive();
            moldBrokenText.SetActive(true);

            brokenMold = swordBrokenMold;
            animBrokenMold = animSwordBrokenMold;
            animBrokenMold.SetActive(true);
            swordBreakAnimator.Play(SWORD_BREAK_ANIM);

            _audioSource.clip = _moldBreak;
            _audioSource.Play();

            StartCoroutine(BreakMold(
                () => IsAnimated(swordBreakAnimator, SWORD_BREAK_ANIM), swordBreakAnimator));
        }
        else if (helmetCounter == 0)
        {
            helmetMoldActive = false;
            helmetCounter = numHelmetsToMoldBreak;
            moldManager.canMakeHelmetMold = true;
            moldInv.SetHelmetInactive();
            moldBrokenText.SetActive(true);

            brokenMold = helmetBrokenMold;
            animBrokenMold = animHelmetBrokenMold;
            animBrokenMold.SetActive(true);
            helmetBreakAnimator.Play(HELMET_BREAK_ANIM);

            _audioSource.clip = _moldBreak;
            _audioSource.Play();

            StartCoroutine(BreakMold(
                () => IsAnimated(helmetBreakAnimator, HELMET_BREAK_ANIM), helmetBreakAnimator));
        }
        else if (arrowCounter == 0)
        {
            arrowMoldActive = false;
            arrowCounter = numArrowsToMoldBreak;
            moldManager.canMakeArrowMold = true;
            moldInv.SetArrowInactive();
            moldBrokenText.SetActive(true);

            brokenMold = arrowBrokenMold;
            animBrokenMold = animArrowBrokenMold;
            animBrokenMold.SetActive(true);
            arrowBreakAnimator.Play(ARROW_BREAK_ANIM);

            _audioSource.clip = _moldBreak;
            _audioSource.Play();

            StartCoroutine(BreakMold(
                () => IsAnimated(arrowBreakAnimator, ARROW_BREAK_ANIM), arrowBreakAnimator));
        }
    }

    IEnumerator BreakMold(Func<bool> condition, Animator anim)
    {
        yield return new WaitForEndOfFrame();

        while (condition())
            yield return null;

        animBrokenMold.SetActive(false);
        brokenMold.SetActive(true);

        yield return new WaitForSeconds(1f);

        brokenMold.SetActive(false);
        moldBrokenText.SetActive(false);
        weaponObj.SetActive(true);
        currentState = BuildState.Inactive;

        ExitGame();
        yield break;
    }
}
