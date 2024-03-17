using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class MoldManager : StationManager
{
    [HideInInspector] public bool canMakeSwordMold;
    [HideInInspector] public bool canMakeHelmetMold;
    [HideInInspector] public bool canMakeArrowMold;

    [Header("Choose Mold UI")]
    // Screen to choose the mold to create
    [SerializeField] GameObject chooseMoldsScreen;
    [SerializeField] GameObject chooseSwordMold;
    [SerializeField] GameObject chooseHelmetMold;
    [SerializeField] GameObject chooseArrowMold;
    [SerializeField] GameObject swordGrayedOutMold;
    [SerializeField] GameObject helmetGrayedOutMold;
    [SerializeField] GameObject arrowGrayedOutMold;

    [Header("Stamps and Slots")]
    // Stamps
    [SerializeField] GameObject swordCutter;
    [SerializeField] GameObject helmetCutter;
    [SerializeField] GameObject arrowCutter;
    // Slots
    [SerializeField] DropPoint3D swordSlot;
    [SerializeField] DropPoint3D helmetSlot;
    [SerializeField] DropPoint3D arrowSlot;

    [Header("Blank and Finished Molds")]
    // Blank molds
    [SerializeField] GameObject blankSwordMold;
    [SerializeField] GameObject blankHelmetMold;
    [SerializeField] GameObject blankArrowMold;
    // Finished mold images
    [SerializeField] GameObject finishedSwordMold;
    [SerializeField] GameObject finishedHelmetMold;
    [SerializeField] GameObject finishedArrowMold;

    private MoldState currentState;
    private DropPoint3D moldSlot = null;
    private GameObject blankMold = null;
    private GameObject moldCutter = null;
    private GameObject finishedMold = null;
    private BuildManager buildManager;
    private bool created = false;

    private enum MoldState
    {
        Inactive,
        ChoosingMold,
        MakingMold
    }

    private void Awake()
    {
        playerSM = FindObjectOfType<PlayerSM>();
        currentState = MoldState.Inactive;
        buildManager = FindObjectOfType<BuildManager>();
        // Default all screens that shouldn't be active
        gameScreen.SetActive(false);
        swordCutter.SetActive(false);
        helmetCutter.SetActive(false);
        arrowCutter.SetActive(false);
        swordSlot.gameObject.SetActive(false);
        helmetSlot.gameObject.SetActive(false);
        arrowSlot.gameObject.SetActive(false);
        blankSwordMold.SetActive(false);
        blankHelmetMold.SetActive(false);
        blankArrowMold.SetActive(false);
        finishedSwordMold.SetActive(false);
        finishedHelmetMold.SetActive(false);
        finishedArrowMold.SetActive(false);

        canMakeSwordMold = false;
        canMakeHelmetMold = false;
        canMakeArrowMold = false;
    }

    public override void StartGame()
    {
        inactive = false;
        created = false;
        currentState = MoldState.ChoosingMold;
        gameScreen.SetActive(true);
        chooseMoldsScreen.SetActive(true);

        // Decide if sword can be made
        if (canMakeSwordMold)
        {
            chooseSwordMold.SetActive(true);
            swordGrayedOutMold.SetActive(false);
        }
        else
        {
            chooseSwordMold.SetActive(false);
            swordGrayedOutMold.SetActive(true);
        }
        // Decide if helmet can be made
        if (canMakeHelmetMold)
        {
            chooseHelmetMold.SetActive(true);
            helmetGrayedOutMold.SetActive(false);
        }
        else
        {
            chooseHelmetMold.SetActive(false);
            helmetGrayedOutMold.SetActive(true);
        }
        // Decide if arrow can be made
        if (canMakeArrowMold)
        {
            chooseArrowMold.SetActive(true);
            arrowGrayedOutMold.SetActive(false);
        }
        else
        {
            chooseArrowMold.SetActive(false);
            arrowGrayedOutMold.SetActive(true);
        }
    }

    private void Update()
    {
        if (currentState == MoldState.Inactive)
            return;
        else if (currentState == MoldState.MakingMold)
        {
            HandleMoldDragInput();
        }
    }

    public void SwordChosen()
    {
        moldCutter = swordCutter;
        finishedMold = finishedSwordMold;
        moldSlot = swordSlot;
        blankMold = blankSwordMold;
        buildManager.swordMoldActive = true;
        canMakeSwordMold = false;
        SwitchToMakeMold();
    }

    public void HelmetChosen()
    {
        moldCutter = helmetCutter;
        finishedMold = finishedHelmetMold;
        moldSlot = helmetSlot;
        blankMold = blankHelmetMold;
        buildManager.helmetMoldActive = true;
        canMakeHelmetMold = false;
        SwitchToMakeMold();
    }

    public void ArrowChosen()
    {
        moldCutter = arrowCutter;
        finishedMold = finishedArrowMold;
        moldSlot = arrowSlot;
        blankMold = blankArrowMold;
        buildManager.arrowMoldActive = true;
        canMakeArrowMold = false;
        SwitchToMakeMold();
    }

    private void SwitchToMakeMold()
    {
        moldSlot.SetSlotName(moldCutter.transform.name);
        chooseMoldsScreen.SetActive(false);
        moldSlot.gameObject.SetActive(true);
        moldCutter.SetActive(true);
        blankMold.SetActive(true);
        currentState = MoldState.MakingMold;
    }

    private void HandleMoldDragInput()
    {
        if (moldSlot.Filled() && !created)
        {
            MakeMold();
        }
    }

    public void MakeMold()
    {
        created = true;
        moldSlot.gameObject.SetActive(false);
        StartCoroutine(WaitAfterCreating());
    }

    IEnumerator WaitAfterCreating()
    {
        blankMold.SetActive(false);
        moldCutter.SetActive(false);
        finishedMold.SetActive(true);
        yield return new WaitForSeconds(1f);
        finishedMold.SetActive(false);
        chooseMoldsScreen.SetActive(true);
        moldSlot.ResetDropPoint();
        currentState = MoldState.Inactive;
        ExitGame();
        yield break;
    }
}
