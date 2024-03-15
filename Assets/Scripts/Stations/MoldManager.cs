using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : StationManager
{
    [HideInInspector] public bool canMakeSwordMold = false;
    [HideInInspector] public bool canMakeHelmetMold = false;
    [HideInInspector] public bool canMakeArrowMold = false;

    [Header("Choose Mold UI")]
    // Screen to choose the mold to create
    [SerializeField] GameObject chooseMoldsScreen;
    [SerializeField] GameObject chooseSwordMold;
    [SerializeField] GameObject chooseHelmetMold;
    [SerializeField] GameObject chooseArrowMold;
    [SerializeField] GameObject swordGrayedOutMold;
    [SerializeField] GameObject helmetGrayedOutMold;
    [SerializeField] GameObject arrowGrayedOutMold;

    [Header("Create Mold UI")]
    // Cutter images and clay to cut
    [SerializeField] GameObject swordCutter;
    [SerializeField] GameObject helmetCutter;
    [SerializeField] GameObject arrowCutter;
    [SerializeField] GameObject clayBlob;
    // Finished mold images
    [SerializeField] GameObject finishedSwordMold;
    [SerializeField] GameObject finishedHelmetMold;
    [SerializeField] GameObject finishedArrowMold;

    private MoldState currentState;
    private GameObject moldCutter = null;
    private GameObject finishedMold = null;
    private BuildManager buildManager;

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
        clayBlob.SetActive(false);
        finishedSwordMold.SetActive(false);
        finishedHelmetMold.SetActive(false);
        finishedArrowMold.SetActive(false);
    }

    public override void StartGame()
    {
        inactive = false;
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
        buildManager.swordMoldActive = true;
        SwitchToMakeMold();
    }

    public void HelmetChosen()
    {
        moldCutter = helmetCutter;
        finishedMold = finishedHelmetMold;
        buildManager.helmetMoldActive = true;
        SwitchToMakeMold();
    }

    public void ArrowChosen()
    {
        moldCutter = arrowCutter;
        finishedMold = finishedArrowMold;
        buildManager.arrowMoldActive = true;
        SwitchToMakeMold();
    }

    private void SwitchToMakeMold()
    {
        clayBlob.GetComponent<DropPoint>().SetSlotName(moldCutter.transform.name);
        chooseMoldsScreen.SetActive(false);
        moldCutter.SetActive(true);
        clayBlob.SetActive(true);
        currentState = MoldState.MakingMold;
    }

    private void HandleMoldDragInput()
    {
        if (clayBlob.GetComponent<DropPoint>().Filled())
        {
            MakeMold();
        }
    }

    public void MakeMold()
    {
        clayBlob.SetActive(false);
        moldCutter.SetActive(false);
        finishedMold.SetActive(true);
        StartCoroutine(WaitAfterCreating());
    }

    IEnumerator WaitAfterCreating()
    {
        yield return new WaitForSeconds(1f);
        finishedMold.SetActive(false);
        chooseMoldsScreen.SetActive(true);
        clayBlob.GetComponent<DropPoint>().ResetDropPoint();
        currentState = MoldState.Inactive;
        ExitGame();
        yield break;
    }
}
